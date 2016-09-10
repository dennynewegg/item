create table svc.PlanFinance( 
	rowid int PRIMARY KEY auto_increment
    ,stockcode char(6) NOT NULL
    ,stockname varchar(6) not null
	,PlanType varchar(10) 
    ,PlanMemo varchar(2000) 
    ,ReportDate datetime not null
    ,Indate datetime not null);

create index Ix_planfinance_stockCode on svc.PlanFinance (stockcode);
create index Ix_planfinance_stockCode_ReportDate on svc.PlanFinance (stockcode,ReportDate);