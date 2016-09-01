CREATE TABLE svc.daily
(
     rowid int PRIMARY KEY auto_increment
    ,stockcode varchar(10) NOT NULL
    ,date int
	,open dec
	,high dec
	,low dec
	,close dec
	,datesort smallint);
    
    create index IX_daily_stockCode on svc.daily (stockcode);
    create index IX_daily_stockCode_date on svc.daily (stockcode,date);
    create index IX_daily_date on svc.daily (date);
    
