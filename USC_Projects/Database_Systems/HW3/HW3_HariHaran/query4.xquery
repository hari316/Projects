SELECT RX.r_title AS BOOK_TITLE, AVG(RX.r_rating) AS AVG_RATING
FROM 
REVIEWS R,
XMLTABLE('for $r in /review return $r' PASSING R.review
          COLUMNS 
          r_title VARCHAR2(30) PATH 'title',
          r_rating VARCHAR2(30) PATH 'rating')RX
GROUP BY RX.r_title
ORDER BY AVG_RATING DESC;