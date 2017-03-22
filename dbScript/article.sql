CREATE TABLE svc.`article` (
  `rowid` int(11) NOT NULL AUTO_INCREMENT,
  Url varchar(200),
  Content Text, 
  InDate datetime,
  PRIMARY KEY (`rowid`),
  KEY `ix_article_Url` (`Url`)
) ENGINE=MyISAM AUTO_INCREMENT=250 DEFAULT CHARSET=utf8;
