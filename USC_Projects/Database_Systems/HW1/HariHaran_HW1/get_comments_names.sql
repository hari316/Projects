SELECT FIRST_NAME, LAST_NAME FROM USERS -
WHERE USERID IN (SELECT DISTINCT C.SENDER_ID -
FROM COMMENTS C -
WHERE C.PID IN (SELECT P.PID FROM POST P -
WHERE P.DATETIME BETWEEN TO_DATE('01/01/2014','MM/DD/YYYY') AND TO_DATE('01/31/2014','MM/DD/YYYY') -
AND P.SHARE_TYPE ='public' -
AND P.SENDER_ID = (SELECT G.GID FROM LGROUP G WHERE G.GNAME = 'USC')));