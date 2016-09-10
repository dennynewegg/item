use svc;

CREATE TABLE svc.RealTime
(
     rowid int PRIMARY KEY auto_increment
    ,stockcode char(6) NOT NULL
    ,InDate  date
    ,close dec(8,2) 
	,open dec(8,2)
	,high dec(8,2) 
	,low dec(8,2) 
    ,Turnover dec(5,2)
    ,Percent dec(5,2)
	,Volume dec(14,0) 
    ,Amount dec(14,0)
	,datesort smallint
    ,InTime Time);
    
    create index IX_RealTime_stockCode on svc.RealTime (stockcode);
    create index IX_RealTime_stockCode_InDate on svc.RealTime (stockcode,InDate);
