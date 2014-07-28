SELECT RX.r_reviwer AS REVIEWER,RX.r_title AS TITLE, RX.r_rating AS RATING, BX.b_pubdate AS PUBLISH_DATE, RX.r_date AS REVIEW_DATE
FROM BOOKS B,
XMLTABLE ('for $b in /book return $b'
          PASSING B.book
          COLUMNS  
          b_title VARCHAR2(50) PATH 'title',
          b_price varchar2(50) PATH 'price',
          b_pubdate DATE PATH 'date') BX,
REVIEWS R,
XMLTABLE('for $r in /review return $r' PASSING R.review
          COLUMNS 
          r_title VARCHAR2(50) PATH 'title',
          r_reviwer VARCHAR2(50) PATH 'reviewer',
          r_rating VARCHAR2(50) PATH 'rating',
          r_date DATE PATH 'date') RX
WHERE    BX.b_title= RX.r_title AND RX.r_rating > 3
ORDER BY PUBLISH_DATE, REVIEW_DATE DESC;