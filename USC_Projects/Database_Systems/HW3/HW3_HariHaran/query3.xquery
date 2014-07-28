SELECT RX.r_reviewer AS REVIEWER, RX.r_title AS TITLE
FROM 
REVIEWS R,
XMLTABLE('for $r in /review return $r' PASSING R.review
          COLUMNS 
          r_reviewer VARCHAR2(30) PATH 'reviewer', 
          r_title VARCHAR2(30) PATH 'title')RX
ORDER BY RX.r_reviewer,RX.r_title;