drop table svc.weekly;


CREATE TABLE svc.`weekly` (
  `rowid` int(11) NOT NULL AUTO_INCREMENT,
  `stockcode` char(6) NOT NULL,
  `sdate` datetime DEFAULT NULL,
  `edate` datetime DEFAULT NULL,
  `close` decimal(8,2) DEFAULT NULL,
  `open` decimal(8,2) DEFAULT NULL,
  `high` decimal(8,2) DEFAULT NULL,
  `low` decimal(8,2) DEFAULT NULL,
  Volume decimal(14,0),
  amount decimal(18,0),
  PRIMARY KEY (`rowid`),
  KEY `IX_weekly_stockCode` (`stockcode`),
  KEY `IX_weekly_stockCode_sDate_edate` (`stockcode`,`sdate`,`edate`)
) ENGINE=MyISAM AUTO_INCREMENT=628405 DEFAULT CHARSET=utf8;
