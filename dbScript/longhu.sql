CREATE TABLE svc.`longhu` (
  `rowid` int(11) NOT NULL AUTO_INCREMENT,
  `stockcode` char(10) NOT NULL,
  `stockname` char(10) NOT NULL,
  `comname` varchar(100) DEFAULT NULL,
  `buyamount` decimal(12,4) DEFAULT NULL,
  `sellamount` decimal(12,4) DEFAULT NULL,
  `netamount` decimal(12,4) DEFAULT NULL,
  Indate datetime,
  PRIMARY KEY (`rowid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
