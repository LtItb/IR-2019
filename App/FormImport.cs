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
using Npgsql;

namespace App
{
    public partial class FormImport : Form
    {
        public FormImport()
        {
            InitializeComponent();
        }

        private void ButtonImport_Click(object sender, EventArgs e)
        {
            string connString = "Host=84.201.147.162;Username=developer;Password=rtfP@ssw0rd;Database=IR-2019";

            //Pretty stuff
            Cursor.Current = Cursors.WaitCursor;
            LabelMessage.Visible = false;
            ButtonImport.Enabled = false;
            LabelError.Visible = false;
            LabelSuccess.Visible = false;

            //Somewhat handle errors
            try {
                var url = TextLink.Text;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(resp.GetResponseStream());
                string data = sr.ReadToEnd();
                string[] array = data.Split(
                    new[] { Environment.NewLine },
                    StringSplitOptions.None
                );
                array = array.Skip(1).ToArray();
                if(array[array.Length-1]=="")
                    Array.Resize(ref array, array.Length - 1);
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    foreach (var element in array)
                    {
                        //Somewhat Parse String
                        var id = element.Substring(0, element.IndexOf(",")).TrimStart('0');
                        var comma_index = element.IndexOf(",");
                        var yearFinder = new Regex(@".+\((\d{4})\)");
                        var title = element.Substring(comma_index + 1);
                        var match = yearFinder.Match(title);
                        var year = 0;
                        if (match.Success)
                            year = int.Parse(match.Groups[1].Value);                        
                        var brace_index = title.IndexOf(" (");
                        var name = brace_index == -1 ? title : title.Substring(0, brace_index);

                        //Execute query
                        var statement = "INSERT INTO movies(id, name, year) VALUES (" + id + ", " + "\'" + name + "\'" + ", " + year + ") ON CONFLICT (id) DO NOTHING";
                        var command = new NpgsqlCommand(statement, conn);
                        command.ExecuteNonQuery();
                    }
                    conn.Close();
                }

                //More pretty stuff
                LabelSuccess.Visible = true;
                ButtonImport.Enabled = true;
            }
            catch (Exception ex)
            {
                //Even more prety stuff
                LabelError.Visible = true;
                ButtonImport.Enabled = true;
                LabelMessage.Text += ex.Message;
                LabelMessage.Visible = true;
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
