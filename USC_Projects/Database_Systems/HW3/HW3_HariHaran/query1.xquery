SELECT DISTINCT RX.r_reviewer AS REVIEWER
FROM BOOKS B,
XMLTABLE ('for $b in /book[25<=price][8<month-from-date(date)] return $b'
          PASSING B.book
          COLUMNS  
          b_title VARCHAR2(30) PATH 'title',
          b_price VARCHAR2(30) PATH 'price',
          b_pubdate DATE PATH 'date') BX,
REVIEWS R,
XMLTABLE ('for $r in /review return $r'
          PASSING R.review
          COLUMNS 
          r_reviewer VARCHAR2(30) PATH 'reviewer', 
          r_title VARCHAR2(30) PATH 'title') RX
WHERE    BX.b_title= RX.r_title;
