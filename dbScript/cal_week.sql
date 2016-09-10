INSERT INTO `svc`.`weekly`
(`stockcode`,
`sdate`,
`edate`,
`close`,
`open`,
`high`,
`low`,
volume,
amount)
SELECT a.stockcode,a.sdate,a.edate,c.close,b.open,a.high,a.low,a.Volume,a.Amount
FROM (

SELECT 
    stockcode,
	MIN(indate) AS sdate,
    MAX(indate) AS edate,
    MAX(high) as high, 
    MIN(low) as low ,
    SUM(Volume) as volume,
    SUM(Amount) as amount
FROM
    svc.daily as a 
WHERE
    indate > '2016/07/08'
GROUP BY stockcode , FLOOR(DATEDIFF(indate, '1989/12/31') / 7)) AS A
INNER JOIN SVC.Daily as b 
on a.stockcode = b.stockcode and a.sdate = b.indate 
inner join svc.daily as c
on a.stockcode = c.stockcode and a.edate = c.indate 

