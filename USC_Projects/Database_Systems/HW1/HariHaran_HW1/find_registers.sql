SELECT U.USERID, U.FIRST_NAME, U.LAST_NAME, U.EMAIL, TO_CHAR(U.REGTIME,'HH24:Mi:SS') AS TIME
FROM USERS U
WHERE U.REGDATE = TO_DATE('01/24/2013','MM/DD/YYYY')
ORDER BY(U.REGTIME) DESC;