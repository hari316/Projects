SELECT U.FIRST_NAME, U.LAST_NAME -
FROM USERS U -
WHERE U.USERID IN -
(SELECT C.SENDER_ID -
FROM USERS U, COMMENTS C, POST P -
WHERE U.USERID = P.SENDER_ID AND U.FIRST_NAME = 'Jackie' AND U.LAST_NAME = 'Chan' -
AND P.PID = C.PID -
GROUP BY C.SENDER_ID
HAVING COUNT(*) > 1);