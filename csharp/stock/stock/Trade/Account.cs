using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
   public class Account
    {
        public decimal Balance { get; private set; }
        private decimal _initBalance;
        public int HoldQty { get; private set; }
        private decimal _commission;
        //private decimal _lastCost;
        private decimal _percent;
        private decimal _buyCost;
        private static ILogWriter Logger = new FileLogWriter("Account"+DateTime.Now.Ticks.ToString()+".txt");

        public ITradeStrategy TradeStrategy { get; private set; }
        public IBuySell Trader { get; set; }

        const decimal _serviceRate = 0.005m;

        public Account(int balance)
        {
            Balance = balance;
            _initBalance = balance;
            TradeStrategy = new TrendStrategy();
            Trader = new FullBuySell();
        }

        public void AddDaily(StockEntity daily)
        {
            if (daily.Percent < -11)
            {
                return;

            }

            _percent = _percent * (1 + daily.Percent.GetValueOrDefault() / 100);

            var vector = TradeStrategy.AddDaily(daily);
            Trader.Trade(this,daily, vector);
        }

        public void Buy(StockEntity daily,int maxQty,decimal cost)
        {
            if(daily.Close>9
                && daily.Close == daily.High)
            {
                return;
            }

            var qty = maxQty / 100 * 100;
            while (qty>0)
            {
                var amount = qty * cost;
                var commission = GetCommission(daily.StockCode, amount, TradeVector.Buy);
                if((Balance-amount-commission)>0)
                {
                    BuyCore(daily, qty, cost);
                    break;
                }
                else
                {
                    qty -= 100;
                }
            }

        }

        public void Sell(StockEntity daily,int qty,decimal cost)
        {
            if(daily.Percent<-9.0m
                && daily.Close == daily.Low)
            {
                return;
            }
            cost =Math.Round( _buyCost * _percent,2);
            SellCore(daily, qty, cost);
        }


        private void BuyCore(StockEntity daily, int qty,decimal cost)
        {
            string stockCode = daily.StockCode;
            var amount = qty * cost;
            var commission = GetCommission(stockCode, amount, TradeVector.Buy);
            _commission = _commission + commission;
            Balance = Balance - amount - commission;
            HoldQty += qty;
            _percent = 1;
            _buyCost = cost;
            Logger.Write(string.Format("buy {0} {3} {2}*{1}  g:{4}"
              , daily.StockCode
              ,cost
              , qty
              ,daily.InDate.Value.ToString("yyyy-MM-dd")
              , Gain()));
        }

        private void SellCore(StockEntity daily, int qty,decimal cost)
        {
            string stockCode = daily.StockCode;
            var amount = qty * cost;
            var commission = GetCommission(stockCode, amount, TradeVector.Sell);
            _commission = _commission + commission;
            Balance = Balance + amount - commission;
            HoldQty -= qty;
            Logger.Write(string.Format("sell {0} {3} {2}*{1}  g:{4}"
              , daily.StockCode
              , cost
              , -qty
              , daily.InDate.Value.ToString("yyyy-MM-dd")
              , Gain()));
        }

        #region
        private decimal GetCommission(string stockCode,decimal amount, TradeVector tradeVector)
        {
            var commission = Math.Min(5, amount * _serviceRate);
            var trade = amount * GetTradeRate(stockCode, tradeVector);
            if(trade > 0)
            {
                trade = Math.Min(5, trade);
            }
            return commission + trade;
        }

        private decimal GetTradeRate(string stockCode, TradeVector tradeVector)
        {
            if (stockCode.StartsWith("6"))
            {
                return 0.005m;
            }
            if(tradeVector== TradeVector.Sell)
            {
                return 0.005m;
            }
            return 0m;
        }
        #endregion

        public decimal Gain()
        {
            return Balance +Math.Round( (_buyCost*_percent * HoldQty),2)-_initBalance;
        }
    }

    public interface IBuySell
    {
        void Trade(Account account, StockEntity daily, TradeVector tradeVector);
    }

    public class FullBuySell : IBuySell
    {
        public void Trade(Account account, StockEntity daily, TradeVector tradeVector)
        {
            if(tradeVector == TradeVector.Buy)
            {
                if (account.HoldQty == 0)
                {
                    account.Buy(daily,(int)(account.Balance/daily.Close.Value), daily.Close.Value);
                }
            }
            else
            {
                if(account.HoldQty >0)
                {
                    account.Sell(daily, account.HoldQty, daily.Close.Value);
                }
            }
        }
    }


    public interface ITradeStrategy
    {
        TradeVector AddDaily(StockEntity stockCode);
    }

    public class AvgTrade : ITradeStrategy
    {
        public List<StockEntity> dailyList = new List<StockEntity>(3000);

        public TradeVector AddDaily(StockEntity daily)
        {
            dailyList.Add(daily);
            var i = dailyList.Count - 1;

            var curr = dailyList[i];
            var step = 5;
            var start = Math.Max(0, i - step + 1);
            var range = dailyList.GetRange(start, i - start + 1);
            curr.AvgK5 = range.Average(item => item.AvgCost);

            step = 10;
            start = Math.Max(0, i - step + 1);
            range = dailyList.GetRange(start, i - start + 1);
            curr.AvgK10 = range.Average(item => item.AvgCost);

            return curr.AvgCost >= curr.AvgK5 && curr.AvgK5 >= curr.AvgK10
                ? TradeVector.Buy
                : TradeVector.Sell;
        }
    }

    public class TrendStrategy : ITradeStrategy
    {
        public List<StockEntity> dailyList = new List<StockEntity>(3000);
        public TradeVector AddDaily(StockEntity daily)
        {
            dailyList.Add(daily);
            decimal total = 0;
            var count = 0;
            for(var loop = 20; loop>0;loop--)
            {
                var index = dailyList.Count - 1 - (20 - loop);
                if(index < 0)
                {
                    break;
                }
                total += loop* dailyList[index].TrendX;
                count += loop;
            }
            daily.TrendY = Math.Round(total / count, 2);
            daily.TrendK5 = dailyList.RangeBefore(5).Average(item => item.TrendY);
            daily.TrendK10 = dailyList.RangeBefore(10).Average(item => item.TrendY);

            return  daily.TrendK5>daily.TrendK10
                ? TradeVector.Buy
                : TradeVector.Sell;
        }
    }

    public class TradeDetail
    {
        public string StockCode { get; set; }
        public string StockName { get; set; }
        public int Qty { get; set; }
        public decimal Cost { get; set; }
    }

    public enum TradeVector
    {
        Buy,
        Sell
    }
}
