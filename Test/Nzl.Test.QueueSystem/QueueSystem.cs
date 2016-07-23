namespace Nzl.Test.QueueSystem
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Nzl.DataStructure.Basic;

    public class QueueSystem
    {
        /// <summary>
        /// 
        /// </summary>
        private DoubleLinkedList<Candidate> _dllCandidate = new DoubleLinkedList<Candidate>();

        /// <summary>
        /// 
        /// </summary>
        private int _totalCount = 0;

        /// <summary>
        /// 
        /// </summary>
        private int _currIndex = 0;

        /// <summary>
        /// 
        /// </summary>
        private int _breakCount = 0;

        /// <summary>
        /// 
        /// </summary>
        private object _lockerForTotalCount = new object();

        /// <summary>
        /// 
        /// </summary>
        private object _lockerForCurrIndex = new object();

        /// <summary>
        /// 
        /// </summary>
        private object _lockerForBreakCount = new object();

        /// <summary>
        /// 
        /// </summary>
        public QueueSystem()
        {

        }

        public int QueueSize
        {
            get
            {
                lock (this._lockerForTotalCount)
                {
                    lock (this._lockerForBreakCount)
                    {
                        lock (this._lockerForCurrIndex)
                        {
                            return this._totalCount - this._breakCount - this._currIndex;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public Candidate AddCandidate(string id)
        {
            if (string.IsNullOrEmpty(id) == false)
            {
                lock (this._dllCandidate)
                {
                    lock (this._lockerForTotalCount)
                    {
                        Candidate candi = new Candidate(this._totalCount, id);
                        this._totalCount++;
                        DoubleLinkedNode<Candidate> dln = new DoubleLinkedNode<Candidate>(candi, candi.Index);
                        this._dllCandidate.Add(dln);
                        return candi;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candi"></param>
        public void BreakCandidate(Candidate candi)
        {
            lock (this._dllCandidate)
            {
                DoubleLinkedNode<Candidate> dln = this._dllCandidate.Find(candi);
                if (dln != null)
                {
                    lock (this._lockerForBreakCount)
                    {
                        this._dllCandidate.Delete(dln);
                        this._breakCount++;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candi"></param>
        public Candidate BreakCandidate(int index)
        {
            lock (this._dllCandidate)
            {
                lock (this._lockerForBreakCount)
                {
                    if (this._dllCandidate.Length > index && index > 0)
                    {
                        DoubleLinkedNode<Candidate> temp = this._dllCandidate.Head;
                        for (int i = 0; i < index; i++)
                        {
                            temp = temp.Next;
                        }

                        this._dllCandidate.Delete(temp);
                        this._breakCount++;
                        return temp.Key;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candi"></param>
        /// <returns></returns>
        public int GetIndex(Candidate candi)
        {
            lock (this._lockerForBreakCount)
            {
                lock (this._lockerForCurrIndex)
                {
                    return candi.Index - this._breakCount - this._currIndex;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Candidate AdmitCandidate()
        {
            lock (this._dllCandidate)
            {
                lock (this._lockerForCurrIndex)
                {
                    if (this._dllCandidate.Length > 0)
                    {
                        Candidate candi = this._dllCandidate.Head.Key;
                        this._dllCandidate.Delete(this._dllCandidate.Head);
                        if (this._dllCandidate.Length > 0)
                        {
                            this._currIndex = System.Convert.ToInt32(this._dllCandidate.Head.Value);
                        }
                        else
                        {
                            this._currIndex = candi.Index + 1;
                        }

                        return candi;
                    }

                    return null;
                }
            }
        }

        public override string ToString()
        {
            lock (this._dllCandidate)
            {
                lock (this._lockerForTotalCount)
                {
                    lock (this._lockerForBreakCount)
                    {
                        lock (this._lockerForCurrIndex)
                        {
                            int len = this._dllCandidate.Length;
                            string allIndex = string.Empty;
                            DoubleLinkedNode<Candidate> tempCandi = this._dllCandidate.Head;
                            if (len > 0)
                            {
                                for(int i=0; i<len; i++)
                                {
                                    allIndex += tempCandi.Key.Index.ToString() + " ";
                                    tempCandi = tempCandi.Next;
                                }
                            }

                            return "Queue Size:\t" + this._dllCandidate.Length
                                 + "\n  Total Count\t" + this._totalCount
                                 + "\n  Break Count\t" + this._breakCount
                                 + "\n  Curr Index\t" + this._currIndex
                                 + "\n  All Index " + allIndex;
                        }
                    }
                }
            }
        }
    }
}