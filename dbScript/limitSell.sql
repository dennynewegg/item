-- drop table svc.limitsell;


CREATE TABLE svc.`limitSell` (
  `rowid` int(11) NOT NULL AUTO_INCREMENT,
  `stockcode` char(6) NOT NULL,
  stockname char(10) null,
  `InDate` datetime DEFAULT NULL,
  `Volume` decimal(14,0) DEFAULT NULL,
  PRIMARY KEY (`rowid`),
  KEY `IX_daily_stockCode` (`stockcode`)
) ENGINE=MyISAM AUTO_INCREMENT=691303 DEFAULT CHARSET=utf8;




