drop table svc.tradetran;


CREATE TABLE svc.`tradetran` (
  `stockcode` varchar(6) not NULL,
  `indate` datetime not  NULL,
  `detail` longtext not null,
  PRIMARY KEY (stockcode,indate)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
