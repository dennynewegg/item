drop table svc.news ;

CREATE TABLE svc.`news` (
  `rowid` int(11) NOT NULL AUTO_INCREMENT,
  `stockcode` char(6) DEFAULT NULL,
  `stockname` varchar(10) DEFAULT NULL,
  `indate` datetime DEFAULT NULL,
  `pubDate` datetime DEFAULT NULL,
  CategoryID varchar(4000),
  CategoryName varchar(4000),
  Memo text,
  url varchar(2000),
  Title varchar(2000),
  PRIMARY KEY (`rowid`),
  KEY `ix_news_stockcode` (`stockcode`),
  KEY `ix_news_stockcode_date` (`stockcode`,indate)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
