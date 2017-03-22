

SELECT 
    a.stockcode,
    c.stockname,
     a.stdp,
    c.marketshare,
    d.yearhigh,
    a.monthhigh,
    h.close,
    h.volume,
    h.Percent,
    g.avgvolume,
	f.eps,
    b.indate AS limitSellDate,
    b.volume / 10000 AS limitshare,
    e.planmemo
from 
(
SELECT stockcode ,std(Percent) as stdp,max(high) as monthhigh,min(low) as low,max(Percent) as percent
FROM SVC.daily  
where indate>date_add(now(),interval -30 day)
group by stockcode ) as a

inner join svc.stock as c
on a.stockcode = c.stockcode
inner join (
SELECT stockcode ,max(high) as yearhigh
FROM SVC.daily  
where indate>date_add(now(),interval -200 day)
group by stockcode ) as d
on a.stockcode = d.stockcode
left join (
SELECT stockcode,min(stockname),sum(Volume) as volume,min(indate) as  indate 
FROM SVC.limitsell 
where indate > date_add(now(),INTERVAL -15 day) 
and indate< date_add(now(),INTERVAL 4 month)
group by stockcode) as b 
on a.stockcode = b.stockcode  
left join(select stockcode,min(planmemo) as planmemo
from svc.planfinance
where reportdate = '2016-09-30'
and plantype in ('预增','预升','预升','减亏')
group by stockcode) as e
on a.stockcode = e.stockcode 
left join svc.finance as f 
on a.stockcode = f.stockcode and f.reportdate = '2016-06-30' 
left join (select stockcode ,avg(Volume) as avgVolume 
from svc.daily 
where indate>date_add(now(),interval -6 day)
group by stockcode
) as g
on a.stockcode = g.stockcode 
inner join (select * from svc.daily 
where indate = (select max(indate) from svc.daily)) as h
on a.stockcode = h.stockcode 
where c.marketshare<1 
-- and (b.stockcode is not null or e.stockcode is not null)
and h.volume > g.avgvolume
and d.yearhigh/a.monthhigh>1.5
order by stdp 


