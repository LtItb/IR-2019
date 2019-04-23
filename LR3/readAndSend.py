import json
import psycopg2
import datetime


#parse whatever date to something good
def parse_date(date_text):
    if date_text is None:
        return datetime.datetime.strptime('01 January 1900', '%d %B %Y').strftime('%Y-%m-%d')
    try:
        return datetime.datetime.strptime(date_text, '%d %B %Y').strftime('%Y-%m-%d')
    except ValueError:
        try:
            return datetime.datetime.strptime('01 ' + date_text, '%d %B %Y').strftime('%Y-%m-%d')
        except ValueError:
            try:
                return datetime.datetime.strptime('01 January' + date_text, '%d %B %Y').strftime('%Y-%m-%d')
            except ValueError:
                return datetime.datetime.strptime('01 January 1900', '%d %B %Y').strftime('%Y-%m-%d')

#get IDs from db
password = open('password.txt', 'r').read()
    
conn = psycopg2.connect(dbname='IR-2019', user='developer', 
                        password=password, host='db.mirvoda.com', port=5454)
cursor = conn.cursor()
cursor.execute('SELECT id FROM movies ORDER BY id ASC')
IDs = cursor.fetchall()
cursor.close()
conn.close()

#and make them into strings
IDstrs = []
for ID in IDs:
    str_id = str(ID)[1:-2] #убираем скобки и запятую
    while(len(str_id) < 7):
        str_id = '0' + str_id
    IDstrs.append(str_id)

#read whatever the spider got us
with open('output.json') as file:
    arr = json.load(file)

complete = {}

#and now we'll make it into real data
for id in IDstrs:
    temp = {}
    temp['id'] = id
    
    #try is here just in case something goes horribly wrong
    #it looks terrible, I know
    try:
        temp['title'] = next(item[id + ' title'] for item in arr if id + ' title' in item)
    except StopIteration:
        temp['title'] = 'No title'
    try:
        year = next(item[id + ' year'] for item in arr if id + ' year' in item)
        temp['year'] = next(item[id + ' year'] for item in arr if id + ' year' in item)
    except StopIteration:
        temp['year'] = 1900
    try:
        temp['rating'] = next(item[id + ' rating'] for item in arr if id + ' rating' in item)
    except StopIteration:
        temp['rating'] = 0.0
    try:
        temp['stars'] = next(item[id + ' stars'] for item in arr if id + ' stars' in item)
    except StopIteration:
        temp['stars'] = 'No stars found'
    try:
        temp['director'] = next(item[id + ' director'] for item in arr if id + ' director' in item)
    except StopIteration:
        temp['director'] = 'No directors found'
    try:
        temp['storyline'] = next(item[id + ' storyline'] for item in arr if id + ' storyline' in item)
    except StopIteration:
        temp['storyline'] = 'No storyline found'
    try:
        temp['genres'] = next(item[id + ' genres'] for item in arr if id + ' genres' in item)
    except StopIteration:
        temp['genres'] = "No genres found"
    try:
        temp['premiere date'] = next(item[id + ' premiere date'] for item in arr if id + ' premiere date' in item)
    except StopIteration:
        temp['premiere date'] = '01 January 1900'
    try:
        temp['synopsis'] = next(item[id + ' synopsis'] for item in arr if id + ' synopsis' in item)
    except StopIteration:
        temp['synopsis'] = 'No synopsis found'
    complete[id] = temp


#new connect
conn = psycopg2.connect(dbname='IR-2019', user='developer', 
                        password=password, host='db.mirvoda.com', port=5454)
cursor = conn.cursor()
    
#and then we just update db
for ID in IDstrs:
    element = complete[ID]
    intID = int(ID)
    date = parse_date(element['premiere date'])
    query = "UPDATE movies SET"
    query = query + " \"Stars\" = %s, \"Director\" = %s, \"Genres\" = %s, \"Storyline\" = %s, \"Synopsis\" = %s, \"Premiere date\" = %s  WHERE id = %s"
    
    #No stars or one star
    if isinstance(element['stars'], str):
        stars = []
        stars.append(element['stars'])
    else:
        stars = list(element['stars'])
    
    #send to db    
    cursor.execute(query, (stars, list(element['director']), list(element['genres']), element['storyline'], element['synopsis'], date, intID))
    conn.commit()
cursor.close()
conn.close()
