select E.fname as Emp_Fname,E.Lname as Emp_Lname,S.fname as Supervisor_Name 
from EMPLOYEE as E,EMPLOYEE as S 
where e.Super_ssn = s.Ssn