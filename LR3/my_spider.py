import scrapy
import psycopg2
import re

class ImdbSpider(scrapy.Spider):
    name = 'imdbspider'
    
    #Здесь нужно или получить массив фильмов из бд\файла, или получить параметр из командной строки
    #и собрать URI
    
    #подключаемся, собираем ID
    password = open('password.txt', 'r').read()
    
    conn = psycopg2.connect(dbname='IR-2019', user='developer', 
                           password=password, host='db.mirvoda.com', port=5454)
    cursor = conn.cursor()
    cursor.execute('SELECT id FROM movies')
    IDs = cursor.fetchall()
    conn.close()
    base_url = 'https://www.imdb.com/title/tt'
    start_urls = []
    for ID in IDs:
        str_id = str(ID)[1:-2] #убираем скобки и запятую
        while(len(str_id) < 7):
            str_id = '0' + str_id
        url = base_url + str_id + '/'  
        start_urls.append(url)
        
    def parse(self, response):
        
        #ID будет меткой у результата
        s = response.url
        reg = '\d{7}'
        p = re.compile(reg)
        result = p.search(s)
        id = result.group(0)

        for title in response.css('.title_wrapper>h1'):
            #название
            yield {id + ' title': title.css('h1 ::text').get().replace(u'\xa0', u'')}
         
        #берем дату премьеры со страницы всех дат   
        for datepage in response.xpath('//h4[text()="Release Date:"]/../span/a/@href').getall():
            #yield {'test' : datepage}
            yield response.follow(datepage, self.parseDate, priority=10)
            
        for title in response.css('.title_wrapper>h1'):
            #год
            yield {id + ' year': title.css('a ::text').get()}
               
        #Пример xpath селекторов
        if not response.css('.ratingValue > strong > span').getall():
            yield {id + ' rating': '0.0'}
        for rating in response.css('.ratingValue > strong > span'):
            #рейтинг
            yield {id + ' rating': rating.css('span ::text').get()}
        #   yield {'ratingXPath': rating.xpath('string(.)').extract()}
            
        #звезды == первые три актера
        if not response.xpath('//h4[text()="Stars:"]/../a').getall():
            yield {id + ' stars': 'No stars found'}
        for stars in response.xpath('//h4[text()="Stars:"]/..'):
            yield {id + ' stars': stars.css('a ::text').getall()}  
            
        #все режиссеры
        if (not response.xpath('//h4[text()="Directors:"]/../a').getall()) and (not response.xpath('//h4[text()="Director:"]/../a').getall()):
            yield {id + ' director': 'No directors found'}
        else:
            for directors in response.xpath('//h4[text()="Directors:"]/..'):
                yield {id + ' director': directors.css('a ::text').getall()}
            for directors in response.xpath('//h4[text()="Director:"]/..'):
                yield {id + ' director': directors.css('a ::text').getall()}  
            
        #сторилайн
        if not response.css('#titleStoryLine > div:nth-child(3) > p > span'):
            yield {id + ' storyline': 'No storyline found'}
        for storyline in response.css('#titleStoryLine > div:nth-child(3) > p > span'):
            yield {id + ' storyline': storyline.css('span ::text').get()}
        
        #жанры
        if not response.xpath('//h4[text()="Genres:"]/../a').getall():
            yield {id + ' genres': 'No genres found'}
        for genres in response.xpath('//h4[text()="Genres:"]/..'):
            yield {id + ' genres': genres.css('a ::text').getall()}  
        
        #синопсис
        for plot_page in response.css('#titleStoryLine > span:nth-child(4) > a:nth-child(3)'):
            #Пример перехода по ссылкам
            yield response.follow(plot_page, self.parseSyno)#Если ссылка найдётся то мы переёдм по ней и вызовем метод parsSyno
            
    #синопсис со страницы синопсисов 
    def parseSyno(self, response):
        s = response.url
        reg = '\d{7}'
        p = re.compile(reg)
        result = p.search(s)
        id = result.group(0)
        if not response.css('#plot-synopsis-content>li'):
            yield{id + ' synopsis' : 'No synopsis found'}
        else:
            for syno in response.css('#plot-synopsis-content>li'):
                yield {id + ' synopsis': syno.xpath('text()').extract()}
    
    #дата со страницы всех премьер
    def parseDate(self, response):
        s = response.url
        reg = '\d{7}'
        p = re.compile(reg)
        result = p.search(s)
        id = result.group(0)
        for td in response.css('#releaseinfo_content > table:nth-child(4) > tr:nth-child(1)'):
            yield {id + ' premiere date': td.css('td:nth-child(2) ::text').get()}
    