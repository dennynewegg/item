drop table finance ;

CREATE TABLE `finance` (
  `rowid` int(11) NOT NULL AUTO_INCREMENT,
  `stockcode` char(6) DEFAULT NULL,
  `stockname` varchar(10) DEFAULT NULL,
  `indate` datetime DEFAULT NULL,
  `reportdate` datetime DEFAULT NULL,
  `eps` decimal(8,3) DEFAULT NULL,
  `CashPerShare` decimal(8,2) DEFAULT NULL,
  `MainIncome` decimal(15,0) DEFAULT NULL,
  `MainProfit` decimal(15,0) DEFAULT NULL,
  `TotalAssets` decimal(15,0) DEFAULT NULL,
  `NetAssets` decimal(15,0) DEFAULT NULL,
  `NetAssPerShare` decimal(8,2) DEFAULT NULL,
  PRIMARY KEY (`rowid`),
  KEY `ix_finance_stockcode` (`stockcode`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
