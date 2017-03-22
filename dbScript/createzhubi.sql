CREATE TABLE `svc`.`zhubi` (
  `StockCode` CHAR(6) NOT NULL,
  `InDate` DATETIME NOT NULL,
  `xls` TEXT NULL,
  PRIMARY KEY (`StockCode`, `InDate`))
ENGINE = MyISAM;
