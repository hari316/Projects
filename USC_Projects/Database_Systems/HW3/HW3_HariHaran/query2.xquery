SELECT DISTINCT BX.b_id AS BOOK_ID, RX.r_title AS TITLE
FROM BOOKS B,
XMLTABLE('for $b in /book return $b' PASSING B.book
          COLUMNS  
          b_id NUMBER(20) PATH 'id',
          b_title VARCHAR2(30) PATH 'title',
          b_price VARCHAR2(30) PATH 'price',
          b_pubdate DATE PATH 'date')BX,
REVIEWS R,
XMLTABLE('for $r in /review[2014=year-from-date(date)] return $r' PASSING R.review
          COLUMNS 
          r_reviewer VARCHAR2(30) PATH 'reviewer', 
          r_title VARCHAR2(30) PATH 'title',
          r_date VARCHAR2(30) PATH 'date')RX

WHERE    BX.b_title= RX.r_title;