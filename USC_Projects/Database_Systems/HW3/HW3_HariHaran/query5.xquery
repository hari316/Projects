SELECT RX.r_reviwer AS REVIEWER, AVG(RX.r_rating) AS AVG_RATING, COUNT(RX.r_rating) AS REVIEW_NUMBER
FROM 
REVIEWS R,
XMLTABLE('for $r in /review return $r' PASSING R.review
          COLUMNS 
          r_reviwer VARCHAR2(30) PATH 'reviewer',
          r_rating VARCHAR2(30) PATH 'rating')RX
GROUP BY RX.r_reviwer
ORDER BY RX.r_reviwer;