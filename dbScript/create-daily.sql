drop database svc;


create database svc;
use svc;



CREATE TABLE svc.daily
(
     rowid int PRIMARY KEY auto_increment
    ,stockcode char(6) NOT NULL
    ,InDate  datetime
    ,close dec(8,2) 
	,open dec(8,2)
	,high dec(8,2) 
	,low dec(8,2) 
    ,Turnover dec(5,2)
    ,Percent dec(5,2)
	,Volume dec(14,0) 
    ,Amount dec(14,0)
	,datesort smallint);
    
    create index IX_daily_stockCode on svc.daily (stockcode);
    create index IX_daily_stockCode_InDate on svc.daily (stockcode,InDate);
    create index IX_daily_InDate on svc.daily (InDate);