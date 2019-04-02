using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Search.Spans;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Npgsql;

namespace App
{
    public partial class FormSearch : Form
    {
        public static LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;

        // Ensures index backwards compatibility (I guess)
        public static String indexLocation = System.IO.Directory.GetCurrentDirectory();
        public static FSDirectory dir = FSDirectory.Open(indexLocation);
        //create an analyzer to process the text
        public static StandardAnalyzer analyzer = new StandardAnalyzer(AppLuceneVersion);

        //create an index writer
        public static IndexWriterConfig indexConfig = new IndexWriterConfig(AppLuceneVersion, analyzer);
        public static IndexWriter writer = new IndexWriter(dir, indexConfig);
     
        public string connString = "Host=db.mirvoda.com;Port=5454;Username=developer;Password=rtfP@ssw0rd;Database=IR-2019";
        public IndexSearcher searcher = new IndexSearcher(writer.GetReader(applyAllDeletes: true));

        public FormSearch()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            //Variables and pretty stuff
            int counter = 0;
            Cursor.Current = Cursors.WaitCursor;
            SearchButton.Enabled = false;
            ResultBox.Items.Clear();
            var query = TextSearch.Text;
            var array = query.Split(' ').ToList();
            List<string> res_list = new List<string>();

            //Some sort of  error handling
            try
            {
                if (!luceneCheck.Checked) 
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        var statement = "";

                        //Поиск по точному названию
                        statement = "SELECT * " +
                                "FROM movies " +
                                "WHERE name = \'" + query + "\'";
                        var command = new NpgsqlCommand(statement, conn);
                        var id = 0;
                        var year = 0;
                        var name = "";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read() && counter < 10)
                            {
                                id = reader.GetInt32(0);
                                year = reader.GetInt32(1);
                                name = reader.GetString(2);
                                counter += 1;
                                res_list.Add("ID: " + id.ToString() + " YEAR: " + year.ToString() + " NAME: " + name);
                            }
                        }

                        //Поиск по году и по названию  //, если предыдущий ничего не дал
                        //if (ResultBox.Items.Count == 0)
                        
                        //Ищем год в запросе
                        string year_to_find = "";
                        int number = 0;
                        foreach (var word in array) {
                            bool result = Int32.TryParse(word, out number);
                            if (result && number > 1800 && number <= 9999) {
                                year_to_find = word;
                                array.RemoveAt(array.IndexOf(word));
                                break;
                            } else number = 0;
                        }

                        //Если нашли
                        if (number != 0)
                            foreach (var word in array)
                            {
                                if (!String.IsNullOrEmpty(word))
                                {
                                    statement = "SELECT * " +
                                        "FROM movies " +
                                        "WHERE year = " + year_to_find + " AND name ILIKE \'%" + word + "%\' ";
                                    command = new NpgsqlCommand(statement, conn);
                                    using (var reader = command.ExecuteReader())
                                    {
                                        while (reader.Read() && counter < 10)
                                        {
                                            counter += 1;
                                            id = reader.GetInt32(0);
                                            year = reader.GetInt32(1);
                                            name = reader.GetString(2);
                                            res_list.Add("ID: " + id.ToString() + " YEAR: " + year.ToString() + " NAME: " + name);
                                        }
                                    }
                                }
                            }

                        //Поиск по слову в названии //, если предыдущие ничего не дали
                        //if (ResultBox.Items.Count == 0)
                        foreach (var word in array)
                        {
                            if (!String.IsNullOrEmpty(word))
                            {
                                statement = "SELECT * " +
                                "FROM movies " +
                                "WHERE name ILIKE \'" + word + " %\' " +
                                    "OR name = \'" + word + "\' " +
                                    "OR  name ILIKE \'% " + word + "\'";
                                command = new NpgsqlCommand(statement, conn);
                                using (var reader = command.ExecuteReader())
                                {
                                    while (reader.Read() && counter < 10)
                                    {
                                        counter += 1;
                                        id = reader.GetInt32(0);
                                        year = reader.GetInt32(1);
                                        name = reader.GetString(2);
                                        res_list.Add("ID: " + id.ToString() + " YEAR: " + year.ToString() + " NAME: " + name);
                                    }
                                }
                            }
                        }

                        //Поиск по части слова в названии. Потому что надо найти хоть что-то
                        //if (ResultBox.Items.Count == 0)
                        foreach (var word in array)
                        {
                            if (!String.IsNullOrEmpty(word))
                            {
                                statement = "SELECT * " +
                                "FROM movies " +
                                "WHERE name ILIKE \'%" + word + "%\' ";
                                command = new NpgsqlCommand(statement, conn);
                                using (var reader = command.ExecuteReader())
                                {
                                    while (reader.Read() && counter < 10)
                                    {
                                        counter += 1;
                                        id = reader.GetInt32(0);
                                        year = reader.GetInt32(1);
                                        name = reader.GetString(2);
                                        res_list.Add("ID: " + id.ToString() + " YEAR: " + year.ToString() + " NAME: " + name);
                                    }
                                }
                            }
                        }

                        //Дубли не хотим
                        res_list = res_list.Select(x => x).Distinct().ToList();
                        ResultBox.Items.Clear();
                        foreach (var item in res_list)
                            ResultBox.Items.Add(item);
                        conn.Close();
                    }
                else
                {
                    //Ищем по одному слову
                    QueryParser parser = new QueryParser(AppLuceneVersion, "name", analyzer);
                    var phrase = new MultiPhraseQuery();
                    foreach (var word in array)
                    {
                        var q = parser.Parse(query);
                        if (!String.IsNullOrEmpty(word))
                        {
                            var res = searcher.Search(q, 10).ScoreDocs;
                            foreach (var hit in res)
                            {
                                var foundDoc = searcher.Doc(hit.Doc);
                                var score = hit.Score;
                                res_list.Add("Score: " + score + " ID: " + foundDoc.GetField("id").GetInt32Value().ToString() +
                                    " YEAR: " + foundDoc.GetField("year").GetInt32Value().ToString() + " NAME: " + foundDoc.GetValues("name")[0]);
                            }
                        }
                    }

                    //Ищем полное название
                    phrase.Add(new Term("name", query));
                    var hits = searcher.Search(phrase, 10).ScoreDocs;
                    foreach (var hit in hits)
                    {
                        var foundDoc = searcher.Doc(hit.Doc);
                        var score = hit.Score;
                        res_list.Add("Score: " + score + " ID: " + foundDoc.GetField("id").GetInt32Value().ToString() +
                            " YEAR: " + foundDoc.GetField("year").GetInt32Value().ToString() + " NAME: " + foundDoc.GetValues("name")[0]);
                    }

                    //Ищем части слов
                    foreach (var word in array)
                    {
                        if (!String.IsNullOrEmpty(word))
                        {
                            var wild = new WildcardQuery(new Term("name", word));
                            var res = searcher.Search(wild, 10).ScoreDocs;
                            foreach (var hit in res)
                            {
                                var foundDoc = searcher.Doc(hit.Doc);
                                var score = hit.Score;
                                res_list.Add("Score: " + score + " ID: " + foundDoc.GetField("id").GetInt32Value().ToString() +
                                    " YEAR: " + foundDoc.GetField("year").GetInt32Value().ToString() + " NAME: " + foundDoc.GetValues("name")[0]);
                            }
                        }
                    }

                    //Ищем год и часть слова
                    string year_to_find = "";
                    int number = 0;
                    foreach (var word in array)
                    {
                        bool result = Int32.TryParse(word, out number);
                        if (result && number > 1800 && number <= 9999)
                        {
                            year_to_find = word;
                            array.RemoveAt(array.IndexOf(word));
                            break;
                        }
                        else number = 0;
                    }

                    //Если нашли
                    if (number != 0)
                    {
                        phrase = new MultiPhraseQuery();
                        foreach (var word in array)
                        {
                            if (!String.IsNullOrEmpty(word))
                            {
                                BooleanQuery booleanQuery = new BooleanQuery();
                                var wild = new WildcardQuery(new Term("name", word));
                                var num = NumericRangeQuery.NewInt32Range("year", 1, number, number, true, true);
                                booleanQuery.Add(wild, Occur.MUST);
                                booleanQuery.Add(num, Occur.MUST);
                                var res = searcher.Search(booleanQuery, 10).ScoreDocs;
                                foreach (var hit in res)
                                {
                                    var foundDoc = searcher.Doc(hit.Doc);
                                    var score = hit.Score;
                                    res_list.Add("Score: " + score + " ID: " + foundDoc.GetField("id").GetInt32Value().ToString() +
                                        " YEAR: " + foundDoc.GetField("year").GetInt32Value().ToString() + " NAME: " + foundDoc.GetValues("name")[0]);
                                }
                            }
                        }
                    }
                }

                //Не хотим дубли
                res_list = res_list.Select(x => x).Distinct().ToList();
                ResultBox.Items.Clear();
                foreach (var item in res_list)
                    ResultBox.Items.Add(item);

                //Ну и если всё плохо
                if (ResultBox.Items.Count == 0)
                    ResultBox.Items.Add("Нет результатов. Попробуйте расширить поисковый запрос");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while searching: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
            SearchButton.Enabled = true;
        }

        private void TextSearch_TextChanged(object sender, EventArgs e)
        {
            //Нельзя искать, если ничего не написано
            SearchButton.Enabled = TextSearch.Text == "" ? false : true;
        }

        private void luceneButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            writer.DeleteAll();
            writer.Flush(triggerMerge: true, applyAllDeletes:true);
            //Yes, I'm handling exceptions
            try
            {
                string connString = "Host=db.mirvoda.com;Port=5454;Username=developer;Password=rtfP@ssw0rd;Database=IR-2019";
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    var statement = "";
                    var id = 0;
                    var year = 0;
                    var name = "";

                    //Hopefully it won't die
                    statement = "SELECT * FROM movies";
                    var command = new NpgsqlCommand(statement, conn);
                    using (var reader = command.ExecuteReader())
                    {
                        //Create documents for searcher 
                        while (reader.Read())
                        {
                            id = reader.GetInt32(0);
                            year = reader.GetInt32(1);
                            name = reader.GetString(2);
                            var source = new
                            {
                                id = id,
                                year = year,
                                name = name
                            };
                            var doc = new Document();
                            doc.Add(new TextField("name", source.name, Field.Store.YES));
                            doc.Add(new StoredField("id", source.id));
                            doc.Add(new Int32Field("year", source.year, Field.Store.YES));
                            writer.AddDocument(doc);
                        }
                    }
                }
                writer.Flush(triggerMerge: false, applyAllDeletes: false);
                writer.Commit();
                luceneCheck.Enabled = true;
                luceneButton.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while downloading data. Message: " + ex.Message, "Error",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                luceneCheck.Enabled = false;
                luceneButton.Enabled = true;
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
