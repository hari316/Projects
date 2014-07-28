create table account (id NUMBER,
                      email VARCHAR2(50) not null,
                      pwd VARCHAR2(50) not null,
                      PRIMARY KEY (id),
                      UNIQUE(email));
                      
Insert into ACCOUNT (ID,EMAIL,PWD) values (1,'ladygaga@xx.com','Pwd@123');
Insert into ACCOUNT (ID,EMAIL,PWD) values (2,'mickey@xx.com','Pwd@124');
Insert into ACCOUNT (ID,EMAIL,PWD) values (3,'imwilliams@yy.com','Pwd@125');
Insert into ACCOUNT (ID,EMAIL,PWD) values (4,'millerl@xx.com','Pwd@126');
Insert into ACCOUNT (ID,EMAIL,PWD) values (5,'robert@xx.com','Pwd@127');
Insert into ACCOUNT (ID,EMAIL,PWD) values (6,'taylor@yy.com','Pwd@128');
Insert into ACCOUNT (ID,EMAIL,PWD) values (7,'mariahall@xx.com','Pwd@129');
Insert into ACCOUNT (ID,EMAIL,PWD) values (8,'jyoung@xx.com','Pwd@130');
Insert into ACCOUNT (ID,EMAIL,PWD) values (9,'lauram@yy.com','Pwd@131');
Insert into ACCOUNT (ID,EMAIL,PWD) values (10,'susanhill@yy.com','Pwd@132');
Insert into ACCOUNT (ID,EMAIL,PWD) values (11,'srt@xy.com','Pwd@133');
Insert into ACCOUNT (ID,EMAIL,PWD) values (12,'rnadal@yy.com','Pwd@134');
Insert into ACCOUNT (ID,EMAIL,PWD) values (13,'woods@xx.com','Pwd@135');
Insert into ACCOUNT (ID,EMAIL,PWD) values (14,'jackiec@xx.com','Pwd@136');
Insert into ACCOUNT (ID,EMAIL,PWD) values (15,'chilinglin@xx.com','Pwd@137');
Insert into ACCOUNT (ID,EMAIL,PWD) values (16,'alberto@xyz.com','Pwd@138');
Insert into ACCOUNT (ID,EMAIL,PWD) values (17,'georgew@yyy.com','Pwd@139');
Insert into ACCOUNT (ID,EMAIL,PWD) values (18,'jose@zz.com','Pwd@140');
Insert into ACCOUNT (ID,EMAIL,PWD) values (19,'roger@yy.com','Pwd@141');
Insert into ACCOUNT (ID,EMAIL,PWD) values (20,'serena@xyz.com','Pwd@142');
Insert into ACCOUNT (ID,EMAIL,PWD) values (4001,'joe@google.com','Pwd@143');
Insert into ACCOUNT (ID,EMAIL,PWD) values (4002,'mack@facebook.com','Pwd@144');
Insert into ACCOUNT (ID,EMAIL,PWD) values (4003,'zack@cisco.com','Pwd@145');
Insert into ACCOUNT (ID,EMAIL,PWD) values (4004,'john@yahoo.com','Pwd@146');
Insert into ACCOUNT (ID,EMAIL,PWD) values (56701,'trojan@usc.edu','Pwd@147');
Insert into ACCOUNT (ID,EMAIL,PWD) values (56702,'bane@bayarea.com','Pwd@148');
Insert into ACCOUNT (ID,EMAIL,PWD) values (56703,'shamsian@database.com','Pwd@149');


create table users (userid NUMBER,
                   first_name VARCHAR2(50) not null,
                   last_name VARCHAR2(50) not null,
                   email VARCHAR2(50),
                   country VARCHAR2(50) not null,
                   zip NUMBER not null,
                   regDate DATE not null,
                   regTime TIMESTAMP not null,
                   status VARCHAR2(50) not null,                  
                   PRIMARY KEY (userid),
                   FOREIGN KEY (userid) REFERENCES account(id)
                   ON DELETE CASCADE
                   );

Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (1,'Lady','Gaga','ladygaga@xx.com','USA',90007,to_date('09-APR-13','DD-MON-RR'),to_timestamp('01-FEB-14 08.15.20.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'),'Employed');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (2,'Michael ','Smith','mickey@xx.com','USA',58493,to_date('19-FEB-13','DD-MON-RR'),to_timestamp('01-FEB-14 07.12.59.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'),'Student');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (3,'Patricia ','Williams','imwilliams@yy.com','USA',42231,to_date('24-JAN-13','DD-MON-RR'),to_timestamp('01-FEB-14 02.00.00.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'Job Seeker');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (4,'Linda ','Miller','millerl@xx.com','USA',61123,to_date('31-DEC-13','DD-MON-RR'),to_timestamp('01-FEB-14 09.30.12.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'),'Employed');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (5,'Robert','Moore','robert@xx.com','USA',36393,to_date('25-OCT-13','DD-MON-RR'),to_timestamp('01-FEB-14 05.33.12.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'Job Seeker');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (6,'David ','Taylor','taylor@yy.com','Ukraine',84489,to_date('08-AUG-13','DD-MON-RR'),to_timestamp('01-FEB-14 06.18.18.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'Student');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (7,'Maria','Hall','mariahall@xx.com','Germany',45898,to_date('07-APR-13','DD-MON-RR'),to_timestamp('01-FEB-14 09.30.00.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'Employed');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (8,'Jennifer ','Young','jyoung@xx.com','Portugal',11276,to_date('02-MAR-13','DD-MON-RR'),to_timestamp('01-FEB-14 10.00.00.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'Employed');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (9,'Laura ','Martin','lauram@yy.com','Croatia',76688,to_date('05-MAY-13','DD-MON-RR'),to_timestamp('01-FEB-14 07.25.54.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'),'Job Seeker');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (10,'Susan','Hill','susanhill@yy.com','Hungary',223344,to_date('20-MAY-13','DD-MON-RR'),to_timestamp('01-FEB-14 06.20.30.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'Student');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (11,'Sachin ','Tendulkar','srt@xy.com','India',651234,to_date('24-JAN-13','DD-MON-RR'),to_timestamp('01-FEB-14 12.30.20.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'Job Seeker');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (12,'Rafael','Nadal','rnadal@yy.com','Spain',775490,to_date('12-SEP-13','DD-MON-RR'),to_timestamp('01-FEB-14 12.30.20.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'Employed');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (13,'Tiger ','Woods','woods@xx.com','USA',101010,to_date('24-NOV-13','DD-MON-RR'),to_timestamp('01-FEB-14 08.30.12.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'),'Student');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (14,'Jackie','Chan','jackiec@xx.com','China',238345,to_date('31-JAN-13','DD-MON-RR'),to_timestamp('01-FEB-14 07.45.54.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'Job Seeker');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (15,'Chiling ','Lin','chilinglin@xx.com','China',565656,to_date('15-JUL-13','DD-MON-RR'),to_timestamp('01-FEB-14 10.10.10.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'),'Student');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (16,'Alberto','Dias','alberto@xyz.com','Brazil',232323,to_date('03-MAR-13','DD-MON-RR'),to_timestamp('01-FEB-14 12.29.30.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'Employed');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (17,'George ','Williams','georgew@yyy.com','UK',876543,to_date('03-MAR-13','DD-MON-RR'),to_timestamp('01-FEB-14 04.21.28.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'Student');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (18,'Nelson ','Jose','jose@zz.com','South Africa',123456,to_date('24-JAN-13','DD-MON-RR'),to_timestamp('01-FEB-14 05.17.17.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'Job Seeker');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (19,'Roger ','Federer','roger@yy.com','Switzerland',546791,to_date('14-OCT-13','DD-MON-RR'),to_timestamp('01-FEB-14 02.11.11.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'Employed');
Insert into USERS (USERID,FIRST_NAME,LAST_NAME,EMAIL,COUNTRY,ZIP,REGDATE,REGTIME,STATUS) values (20,'Serena ','Williams','serena@xyz.com','USA',192837,to_date('26-SEP-13','DD-MON-RR'),to_timestamp('01-FEB-14 08.20.30.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'),'Student');

                   
create table lgroup (gid NUMBER,
                    gname VARCHAR2(50) not null,
                    PRIMARY KEY (gid),
                    FOREIGN KEY (gid) REFERENCES account(id)
                    ON DELETE CASCADE
                    );
                    
Insert into LGROUP (GID,GNAME) values (56701,'USC');
Insert into LGROUP (GID,GNAME) values (56702,'Bay Area');
Insert into LGROUP (GID,GNAME) values (56703,'Database System');

create table company (cid NUMBER,
                    cname VARCHAR2(50) not null,
                    PRIMARY KEY (cid),
                    FOREIGN KEY (cid) REFERENCES account(id)
                    ON DELETE CASCADE
                    );

Insert into COMPANY (CID,CNAME) values (4001,'Google Inc');
Insert into COMPANY (CID,CNAME) values (4002,'Facebook');
Insert into COMPANY (CID,CNAME) values (4003,'CISCO');
Insert into COMPANY (CID,CNAME) values (4004,'Yahoo.com');
                    
create table usergrp (userid NUMBER not null,
                    grpid NUMBER not null,
                    datetime TIMESTAMP not null,
                    PRIMARY KEY (userid,grpid),
                    FOREIGN KEY (userid) REFERENCES users(userid),
                    FOREIGN KEY (grpid) REFERENCES lgroup(gid)
                    ON DELETE CASCADE
                    ); 

Insert into USERGRP (USERID,GRPID,DATETIME) values (1,56701,to_timestamp('16-DEC-13 03.32.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into USERGRP (USERID,GRPID,DATETIME) values (5,56701,to_timestamp('18-DEC-13 08.15.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into USERGRP (USERID,GRPID,DATETIME) values (9,56701,to_timestamp('18-DEC-13 06.22.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into USERGRP (USERID,GRPID,DATETIME) values (19,56701,to_timestamp('24-DEC-13 08.20.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into USERGRP (USERID,GRPID,DATETIME) values (11,56701,to_timestamp('30-DEC-13 12.30.00.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into USERGRP (USERID,GRPID,DATETIME) values (16,56701,to_timestamp('04-JAN-14 04.30.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into USERGRP (USERID,GRPID,DATETIME) values (7,56701,to_timestamp('05-JAN-14 08.30.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into USERGRP (USERID,GRPID,DATETIME) values (12,56701,to_timestamp('05-JAN-14 12.30.00.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into USERGRP (USERID,GRPID,DATETIME) values (15,56701,to_timestamp('19-DEC-13 08.15.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into USERGRP (USERID,GRPID,DATETIME) values (8,56701,to_timestamp('04-JAN-14 08.15.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into USERGRP (USERID,GRPID,DATETIME) values (2,56701,to_timestamp('05-JAN-14 08.15.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into USERGRP (USERID,GRPID,DATETIME) values (13,56701,to_timestamp('05-JAN-14 08.15.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into USERGRP (USERID,GRPID,DATETIME) values (16,56702,to_timestamp('05-JAN-14 08.15.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into USERGRP (USERID,GRPID,DATETIME) values (1,56702,to_timestamp('05-JAN-14 08.15.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into USERGRP (USERID,GRPID,DATETIME) values (17,56702,to_timestamp('05-JAN-14 08.15.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into USERGRP (USERID,GRPID,DATETIME) values (16,56703,to_timestamp('16-DEC-13 03.32.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into USERGRP (USERID,GRPID,DATETIME) values (1,56703,to_timestamp('16-DEC-12 03.32.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
                    
create table follower (userid NUMBER not null,
                    cid NUMBER not null,
                    datetime TIMESTAMP not null,
                    PRIMARY KEY (userid,cid),
                    FOREIGN KEY (userid) REFERENCES users(userid),
                    FOREIGN KEY (cid) REFERENCES company(cid)
                    ON DELETE CASCADE
                    );

Insert into FOLLOWER (USERID,CID,DATETIME) values (12,4002,to_timestamp('16-DEC-12 03.32.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into FOLLOWER (USERID,CID,DATETIME) values (17,4004,to_timestamp('17-DEC-12 03.32.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into FOLLOWER (USERID,CID,DATETIME) values (2,4002,to_timestamp('18-DEC-12 03.32.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into FOLLOWER (USERID,CID,DATETIME) values (10,4001,to_timestamp('19-DEC-12 03.32.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into FOLLOWER (USERID,CID,DATETIME) values (4,4002,to_timestamp('20-DEC-12 03.32.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));

                    
create table user_conn (userid NUMBER NOT NULL,
                    connectid NUMBER NOT NULL,
                    PRIMARY KEY (userid,connectid),
                    FOREIGN KEY (userid) REFERENCES users(userid),
                    FOREIGN KEY (connectid) REFERENCES users(userid)
                    ON DELETE CASCADE 
                    );     

Insert into USER_CONN (USERID,CONNECTID) values (1,3);
Insert into USER_CONN (USERID,CONNECTID) values (3,6);
Insert into USER_CONN (USERID,CONNECTID) values (5,20);
Insert into USER_CONN (USERID,CONNECTID) values (7,1);
Insert into USER_CONN (USERID,CONNECTID) values (7,14);
Insert into USER_CONN (USERID,CONNECTID) values (9,3);
Insert into USER_CONN (USERID,CONNECTID) values (9,7);
Insert into USER_CONN (USERID,CONNECTID) values (14,5);
Insert into USER_CONN (USERID,CONNECTID) values (16,18);
Insert into USER_CONN (USERID,CONNECTID) values (19,12);
Insert into USER_CONN (USERID,CONNECTID) values (19,20);
Insert into USER_CONN (USERID,CONNECTID) values (20,12);
                    
create table resources (rsid NUMBER,
                    r_type VARCHAR2(50) NOT NULL,
                    r_link VARCHAR2(50) NOT NULL,
                    PRIMARY KEY (rsid)
                    );     
                    
Insert into RESOURCES (RSID,R_TYPE,R_LINK) values (8001,'image','image8001');
Insert into RESOURCES (RSID,R_TYPE,R_LINK) values (8002,'image','image8002');
Insert into RESOURCES (RSID,R_TYPE,R_LINK) values (8003,'presentation','presentation8003');
Insert into RESOURCES (RSID,R_TYPE,R_LINK) values (8004,'survey','survey8004');
Insert into RESOURCES (RSID,R_TYPE,R_LINK) values (8005,'presentation','presentation8005');
Insert into RESOURCES (RSID,R_TYPE,R_LINK) values (8006,'presentation','presentation8006');
Insert into RESOURCES (RSID,R_TYPE,R_LINK) values (8007,'image','image8007');
Insert into RESOURCES (RSID,R_TYPE,R_LINK) values (8008,'survey','survey8008');
Insert into RESOURCES (RSID,R_TYPE,R_LINK) values (8009,'image','image8009');
Insert into RESOURCES (RSID,R_TYPE,R_LINK) values (8010,'presentation','presentation8010');
Insert into RESOURCES (RSID,R_TYPE,R_LINK) values (8011,'presentation','presentation8011');


create table user_resource (userid NUMBER NOT NULL,
                    rsid NUMBER NOT NULL,
                    PRIMARY KEY (userid,rsid),
                    FOREIGN KEY (userid) REFERENCES users(userid),
                    FOREIGN KEY (rsid) REFERENCES resources(rsid)
                    ON DELETE CASCADE 
                    );

Insert into USER_RESOURCE (USERID,RSID) values (1,8001);
Insert into USER_RESOURCE (USERID,RSID) values (1,8006);
Insert into USER_RESOURCE (USERID,RSID) values (2,8003);
Insert into USER_RESOURCE (USERID,RSID) values (3,8003);
Insert into USER_RESOURCE (USERID,RSID) values (4,8004);
Insert into USER_RESOURCE (USERID,RSID) values (4,8006);
Insert into USER_RESOURCE (USERID,RSID) values (5,8002);
Insert into USER_RESOURCE (USERID,RSID) values (6,8006);
Insert into USER_RESOURCE (USERID,RSID) values (6,8008);
Insert into USER_RESOURCE (USERID,RSID) values (7,8006);
Insert into USER_RESOURCE (USERID,RSID) values (7,8007);
Insert into USER_RESOURCE (USERID,RSID) values (8,8002);
Insert into USER_RESOURCE (USERID,RSID) values (8,8005);
Insert into USER_RESOURCE (USERID,RSID) values (8,8008);
Insert into USER_RESOURCE (USERID,RSID) values (9,8003);
Insert into USER_RESOURCE (USERID,RSID) values (9,8008);
Insert into USER_RESOURCE (USERID,RSID) values (10,8006);
Insert into USER_RESOURCE (USERID,RSID) values (10,8010);
Insert into USER_RESOURCE (USERID,RSID) values (11,8001);
Insert into USER_RESOURCE (USERID,RSID) values (11,8008);
Insert into USER_RESOURCE (USERID,RSID) values (12,8003);
Insert into USER_RESOURCE (USERID,RSID) values (13,8006);
Insert into USER_RESOURCE (USERID,RSID) values (13,8011);
Insert into USER_RESOURCE (USERID,RSID) values (14,8003);
Insert into USER_RESOURCE (USERID,RSID) values (15,8006);
Insert into USER_RESOURCE (USERID,RSID) values (15,8010);
Insert into USER_RESOURCE (USERID,RSID) values (16,8001);
Insert into USER_RESOURCE (USERID,RSID) values (16,8011);
Insert into USER_RESOURCE (USERID,RSID) values (17,8004);
Insert into USER_RESOURCE (USERID,RSID) values (17,8007);
Insert into USER_RESOURCE (USERID,RSID) values (18,8003);
Insert into USER_RESOURCE (USERID,RSID) values (19,8004);
Insert into USER_RESOURCE (USERID,RSID) values (19,8006);
Insert into USER_RESOURCE (USERID,RSID) values (19,8009);
Insert into USER_RESOURCE (USERID,RSID) values (20,8001);
Insert into USER_RESOURCE (USERID,RSID) values (20,8011);

create table post ( pid NUMBER,
                  sender_id NUMBER not null,
                  receiver_id NUMBER not null,
                  post_type VARCHAR2(50) not null,
                  post_content VARCHAR2(50),
                  share_type VARCHAR2(50) not null,
                  attach_res NUMBER,
                  datetime TIMESTAMP not null,   
                  PRIMARY KEY (pid),
                  FOREIGN KEY (sender_id) REFERENCES account(id),
                  FOREIGN KEY (receiver_id) REFERENCES account(id),
                  FOREIGN KEY (attach_res) REFERENCES resources(rsid) 
                  );
                    
Insert into POST (PID,SENDER_ID,RECEIVER_ID,POST_TYPE,POST_CONTENT,SHARE_TYPE,ATTACH_RES,DATETIME) values (101,19,20,'connection','FB CEO Resigns','public',8009,to_timestamp('16-DEC-13 03.32.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into POST (PID,SENDER_ID,RECEIVER_ID,POST_TYPE,POST_CONTENT,SHARE_TYPE,ATTACH_RES,DATETIME) values (102,9,3,'connection','Yahoo hiring interns','connection',null,to_timestamp('18-DEC-13 08.15.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into POST (PID,SENDER_ID,RECEIVER_ID,POST_TYPE,POST_CONTENT,SHARE_TYPE,ATTACH_RES,DATETIME) values (103,11,56701,'group','Tech jobs rush','public',8001,to_timestamp('18-DEC-13 06.22.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into POST (PID,SENDER_ID,RECEIVER_ID,POST_TYPE,POST_CONTENT,SHARE_TYPE,ATTACH_RES,DATETIME) values (104,16,56702,'group','See you at career fair','public',8011,to_timestamp('24-DEC-13 08.20.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into POST (PID,SENDER_ID,RECEIVER_ID,POST_TYPE,POST_CONTENT,SHARE_TYPE,ATTACH_RES,DATETIME) values (105,1,3,'connection','are you ready','connection',null,to_timestamp('30-DEC-13 12.30.00.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into POST (PID,SENDER_ID,RECEIVER_ID,POST_TYPE,POST_CONTENT,SHARE_TYPE,ATTACH_RES,DATETIME) values (106,9,3,'connection','Google glasses','public',8008,to_timestamp('04-JAN-14 04.30.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into POST (PID,SENDER_ID,RECEIVER_ID,POST_TYPE,POST_CONTENT,SHARE_TYPE,ATTACH_RES,DATETIME) values (107,7,56701,'group','Trojans hiring','public',null,to_timestamp('05-JAN-14 08.30.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into POST (PID,SENDER_ID,RECEIVER_ID,POST_TYPE,POST_CONTENT,SHARE_TYPE,ATTACH_RES,DATETIME) values (108,10,4002,'company','is FB using open graphs?','public',null,to_timestamp('05-JAN-14 12.30.00.000000000 PM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into POST (PID,SENDER_ID,RECEIVER_ID,POST_TYPE,POST_CONTENT,SHARE_TYPE,ATTACH_RES,DATETIME) values (109,14,3,'connection','Google hiring interns','connection',null,to_timestamp('19-DEC-13 08.15.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into POST (PID,SENDER_ID,RECEIVER_ID,POST_TYPE,POST_CONTENT,SHARE_TYPE,ATTACH_RES,DATETIME) values (110,14,10,'connection','Amazon hiring interns','connection',null,to_timestamp('04-JAN-14 08.15.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into POST (PID,SENDER_ID,RECEIVER_ID,POST_TYPE,POST_CONTENT,SHARE_TYPE,ATTACH_RES,DATETIME) values (111,11,14,'connection','Wow! Amazing!','connection',null,to_timestamp('05-JAN-14 08.15.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into POST (PID,SENDER_ID,RECEIVER_ID,POST_TYPE,POST_CONTENT,SHARE_TYPE,ATTACH_RES,DATETIME) values (112,11,14,'connection','Wow! Superb!','connection',null,to_timestamp('05-JAN-14 08.15.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into POST (PID,SENDER_ID,RECEIVER_ID,POST_TYPE,POST_CONTENT,SHARE_TYPE,ATTACH_RES,DATETIME) values (113,56701,56701,'group','USC welcomes you!','connection',null,to_timestamp('05-JAN-14 08.15.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into POST (PID,SENDER_ID,RECEIVER_ID,POST_TYPE,POST_CONTENT,SHARE_TYPE,ATTACH_RES,DATETIME) values (114,56701,56701,'group','Fight on!','connection',null,to_timestamp('05-JAN-14 08.15.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));
Insert into POST (PID,SENDER_ID,RECEIVER_ID,POST_TYPE,POST_CONTENT,SHARE_TYPE,ATTACH_RES,DATETIME) values (115,56701,56701,'group','Fight on!','public',null,to_timestamp('05-JAN-14 08.15.00.000000000 AM','DD-MON-RR HH.MI.SSXFF AM'));


create table comments ( commentid number,
                  sender_id NUMBER not null,
                  pid NUMBER not null,
                  c_content VARCHAR2(50),
                  islike NUMBER,
                  isshare NUMBER,
                  PRIMARY KEY(commentid),
                  FOREIGN KEY (sender_id) REFERENCES account(id),
                  FOREIGN KEY (pid) REFERENCES post(pid)
                  );

Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (701,12,103,'Which campanies coming?',1,1);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (702,20,101,'Really? When?',1,0);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (703,15,103,'JP Morgan',1,0);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (704,6,102,'From USC!',0,0);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (705,12,101,'Today',1,1);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (706,17,104,'When is Career fair?',1,0);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (707,12,103,'Bloomburg',0,1);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (708,1,104,'Its next Wednesday',1,1);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (709,8,107,' Trojans hiring trojans',1,0);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (710,9,107,'Startups!',0,1);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (711,3,106,'I love google glasses',0,1);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (712,8,107,'Big companies',1,0);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (713,14,108,'FB using RDF',1,0);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (714,15,108,'RDF to link data',1,1);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (715,15,111,'Linked Data!',1,1);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (716,15,111,'Congrats!',1,1);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (717,8,111,'Congrats! Well done!',1,1);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (718,15,111,'Great! Congrats!',1,1);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (719,15,113,'Great! Thanks for info.',1,1);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (720,14,113,'Great! Thank you!',1,1);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (721,14,114,'Great! Thanks man!',1,1);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (722,14,114,'Great! Thanks dude!',1,1);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (723,14,115,'Thanks! P',1,1);
Insert into COMMENTS (COMMENTID,SENDER_ID,PID,C_CONTENT,ISLIKE,ISSHARE) values (724,14,115,'Great! Thanks a ton!',1,1);
