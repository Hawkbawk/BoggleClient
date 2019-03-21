// Initial version written by Joseph Zachary for CS 3500
// Copyright Joseph Zachary, March 2019

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReaderWriterLockClasses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReaderWriterUnitTests
{
    [TestClass]
    public class RWLockTests
    {
        /// <summary>
        /// Verifies that an attempt to exit a write lock without having previously acquired a write
        /// lock results in a SynchronizationLockException. 
        /// </summary>
        [TestMethod, Timeout(500)]
        [ExpectedException(typeof(SynchronizationLockException))]
        public void TestExitWriteLockWithoutEnter()
        {
            // This is how you should create a lock in all of your test cases.
            // Do not create locks any other way or your code will be ungradeable.
            RWLock rwLock = RWLockBuilder.NewLock();

            // This should result in an exception because a write lock was never acquired.
            rwLock.ExitWriteLock();
        }

        /// <summary>
        /// Verifies that two tasks can simultaneously acquire the same read lock.
        /// </summary>
        [TestMethod, Timeout(500)]
        public void TestObtainSimultaneousReadLock()
        {
            // These local variables are used by the GetReadLock method below.  They are accessible
            // to that method because it is nested within TestMethod2.
            int count = 2;
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();

            // Run GetReadLock() on two tasks.  Wait up to one second for count to be decremented to zero.
            Task t1 = Task.Run(() => GetReadLock());
            Task t2 = Task.Run(() => GetReadLock());
            Assert.IsTrue(SpinWait.SpinUntil(() => count == 0, 1000), "Unable to have two simultaneous readers");

            // Allow the blocked tasks to resume, which will result in their termination
            mre.Set();

            // This method is run simultaneously in two tasks
            void GetReadLock()
            {
                // Acquire a read lock
                rwLock.EnterReadLock();

                // Atomically decrement the shared count variable.  Note that merely doing count-- won't always work.
                Interlocked.Decrement(ref count);

                // Block until the main task sets the mre to true.
                mre.WaitOne();

                // Exit the read lock
                rwLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Ensures that the IsReadLockHeld instance variable is updating correctly and performing as specified.
        /// </summary>
        [TestMethod]
        public void TestIsReadLockHeldMultiThread()
        {
            // These local variables are used by the GetReadLock method below.  They are accessible
            // to that method because it is nested within TestMethod2.
            int count = 2;

            int EnterReadLockCount = 0;
            int ExitReadLockCount = 0;
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();

            // Run GetReadLock() on two tasks.  Wait up to one second for count to be decremented to zero.
            Task t1 = Task.Run(() => GetReadLock());
            Task t2 = Task.Run(() => GetReadLock());
            Assert.IsTrue(SpinWait.SpinUntil(() => count == 0, 1000), "Unable to have two simultaneous readers");

            Assert.AreEqual(2, EnterReadLockCount);
            Assert.AreEqual(2, ExitReadLockCount);

            // Allow the blocked tasks to resume, which will result in their termination
            mre.Set();


            // This method is run simultaneously in two tasks
            void GetReadLock()
            {
                // Acquire a read lock
                rwLock.EnterReadLock();

                if (rwLock.IsReadLockHeld)      //Check if read lock is currently on
                {
                    Interlocked.Increment(ref EnterReadLockCount);      //Atomically increments the shared counter that keeps track of the amount of times there was a read lock\
                }
                // Atomically decrement the shared count variable.  Note that merely doing count-- won't always work.
                Interlocked.Decrement(ref count);


                // Block until the main task sets the mre to true.

                // Exit the read lock
                rwLock.ExitReadLock();

                if (!rwLock.IsReadLockHeld)     //check if read lock is currently off
                {
                    Interlocked.Increment(ref ExitReadLockCount);   //Atomically increments the shared counter that keeps track of the amount of times there was a read lock
                }
                mre.WaitOne();
            }
        }

        /// <summary>
        /// Uses GetReadCount method to check if there are actually 3 threads that have entered the
        /// lock in read mode.
        /// </summary>
        [TestMethod, Timeout(200)]
        public void TestCurrentReadCountThreeThreads()
        {
            // These local variables are used by the GetReadLock method below.  They are accessible
            // to that method because it is nested within TestMethod2.
            int count = 3;
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();

            // Run GetReadLock() on three tasks.  Wait up to one second for count to be decremented to zero.
            Task t1 = Task.Run(() => GetReadLock());
            Task t2 = Task.Run(() => GetReadLock());
            Task t3 = Task.Run(() => GetReadLock());
            Assert.IsTrue(SpinWait.SpinUntil(() => count == 0, 1000), "Unable to have three simultaneous readers");

            Assert.AreEqual(3, rwLock.CurrentReadCount);

            // Allow the blocked tasks to resume, which will result in their termination
            mre.Set();

            // This method is run simultaneously in two tasks
            void GetReadLock()
            {
                // Acquire a read lock
                rwLock.EnterReadLock();

                // Atomically decrement the shared count variable.  Note that merely doing count-- won't always work.
                Interlocked.Decrement(ref count);



                // Block until the main task sets the mre to true.
                mre.WaitOne();

                // Exit the read lock
                rwLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Uses the GetReadCount method to check if there are actually 2 threads that have entered
        /// the lock in read mode.
        /// </summary>
        [TestMethod, Timeout(500)]
        public void TestCurrentReadCount()
        {
            // These local variables are used by the GetReadLock method below.  They are accessible
            // to that method because it is nested within TestMethod2.
            int count = 2;
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();

            // Run GetReadLock() on two tasks.  Wait up to one second for count to be decremented to zero.
            Task t1 = Task.Run(() => GetReadLock());
            Task t2 = Task.Run(() => GetReadLock());
            Assert.IsTrue(SpinWait.SpinUntil(() => count == 0, 1000), "Unable to have two simultaneous readers");

            // Assert that two unique threads entered the read lock.
            Assert.AreEqual(2, rwLock.CurrentReadCount);

            // Allow the blocked tasks to resume, which will result in their termination
            mre.Set();

            // This method is run simultaneously in two tasks
            void GetReadLock()
            {
                // Acquire a read lock
                rwLock.EnterReadLock();

                // Atomically decrement the shared count variable.  Note that merely doing count-- won't always work.
                Interlocked.Decrement(ref count);

                // Block until the main task sets the mre to true.
                mre.WaitOne();

                // Exit the read lock
                rwLock.ExitReadLock();
            }
        }


        /// <summary>
        /// Ensures that the IsWriteLockHeld instance variable is performing as expected.
        /// </summary>
        [TestMethod, Timeout(200)]
        public void TestIsWriteLockHeldMultiThread()
        {
            // These local variables are used by the GetReadLock method below.  They are accessible
            // to that method because it is nested within TestMethod2.
            int count = 2;

            int EnterWriteLockCount = 0;
            int ExitWriteLockCount = 0;
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();

            // Run GetWriteLock() on two tasks.  Wait up to one second for count to be decremented to zero.
            Task t1 = Task.Run(() => GetWriteLock());
            Task t2 = Task.Run(() => GetWriteLock());
            Assert.IsTrue(SpinWait.SpinUntil(() => count == 0, 1000), "Unable to have two simultaneous writers");


            Assert.AreEqual(2, EnterWriteLockCount);
            Assert.AreEqual(2, ExitWriteLockCount);

            // Allow the blocked tasks to resume, which will result in their termination
            mre.Set();

            void GetWriteLock()
            {
                rwLock.EnterWriteLock();

                // Increment the EnterWriteLockCount Variable if the WriteLock is held.
                if (rwLock.IsWriteLockHeld)
                {
                    Interlocked.Increment(ref EnterWriteLockCount);
                }

                Interlocked.Decrement(ref count);

                rwLock.ExitWriteLock();

                if (!rwLock.IsWriteLockHeld)     //check if read lock is currently off
                {
                    Interlocked.Increment(ref ExitWriteLockCount);   //Atomically increments the shared counter that keeps track of the amount of times there was a read lock
                }
                mre.WaitOne();
            }
        }

        /// <summary>
        /// Verifies that two tasks can simultaneously acquire the same read lock... 
        /// 
        /// TESTS WaitingWriteCount
        /// </summary>
        [TestMethod, Timeout(500)]
        public void TestWaitingWriteCountWorks()
        {
            // These local variables are used by the GetReadLock method below.  They are accessible
            // to that method because it is nested within TestMethod2.
            int WaitingForLock = 0;
            int NoLongerWaiting = 2;
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();
            
            Task t1 = Task.Run(() => GetWriteLock());


            Assert.IsTrue(SpinWait.SpinUntil(() => WaitingForLock == 2 && NoLongerWaiting == 0, 1000), " ");

            mre.Set();
            void GetWriteLock()
            {
                rwLock.EnterWriteLock();
                Task t2 = Task.Run(() => WaitForWriteLock());
                Task t3 = Task.Run(() => WaitForWriteLock());
                if (SpinWait.SpinUntil(() => rwLock.WaitingWriteCount == 2, 500))
                {
                    WaitingForLock = rwLock.WaitingWriteCount;
                }
                rwLock.ExitWriteLock();
                if (SpinWait.SpinUntil(() => rwLock.WaitingWriteCount == 0, 500))
                {
                    NoLongerWaiting = 0;
                }
                mre.WaitOne();
            }

            void WaitForWriteLock()
            {
                rwLock.EnterWriteLock();
                rwLock.ExitWriteLock();
            }
        }


        /// <summary>
        /// Ensures that the WaitingReadCount instance variable is updating as expected. Runs twenty
        /// times to ensure that no odd multithreading stuff is making the test pass.
        /// </summary>
        [TestMethod]
        public void TestWaitingReadCountWorks()
        {
            // These local variables are used by the GeReadLock method below.  They are accessible
            // to that method because it is nested within this test case.

            int WaitingForLock = 0;
            int NoLongerWaiting = 2;
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();
            Task t1 = Task.Run(() => GetReadLock());
            
            
            Assert.IsTrue(SpinWait.SpinUntil(() => WaitingForLock == 2 && NoLongerWaiting == 0, 1000), " ");
            mre.Set();
            void GetReadLock()
            {
                rwLock.EnterWriteLock();
                Task t2 = Task.Run(() => WaitForReadLock());
                Task t3 = Task.Run(() => WaitForReadLock());
                if (SpinWait.SpinUntil(() => rwLock.WaitingReadCount == 2, 500))
                {
                    WaitingForLock = 2;
                }
                rwLock.ExitWriteLock();
                if (SpinWait.SpinUntil(() => rwLock.WaitingReadCount == 0, 500))
                {
                    NoLongerWaiting = 0;
                }
                mre.WaitOne();
            }

            void WaitForReadLock()
            {
                rwLock.EnterReadLock();
                rwLock.ExitReadLock();
            }

        }


        [TestMethod, Timeout(500)]
        [ExpectedException(typeof(LockRecursionException))]
        public void TestMultipleEnterReadLock()
        {
            RWLock rw = RWLockBuilder.NewLock();
            rw.EnterReadLock();
            rw.EnterReadLock();
        }


        [TestMethod, Timeout(500)]
        [ExpectedException(typeof(LockRecursionException))]
        public void TestMultipleEnterWriteLock()
        {
            RWLock rw = RWLockBuilder.NewLock();
            rw.EnterWriteLock();
            rw.EnterWriteLock();
        }

        [TestMethod, Timeout(500)]
        [ExpectedException(typeof(SynchronizationLockException))]
        public void TestExitReadWithoutEnter()
        {
            RWLock rw = RWLockBuilder.NewLock();
            rw.ExitReadLock();
        }


        [TestMethod, Timeout(500)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestTryEnterReadLockNegativeInput()
        {
            // These local variables are used by the GetReadLock method below.  They are accessible
            // to that method because it is nested within TestMethod2.
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();

            rwLock.TryEnterReadLock(-120000);
            rwLock.ExitReadLock();
        }

        [TestMethod, Timeout(500)]
        [ExpectedException(typeof(LockRecursionException))]
        public void TestTryEnterReadLockLockRecursion()
        {
            // These local variables are used by the GetReadLock method below.  They are accessible
            // to that method because it is nested within TestMethod2.
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();

            rwLock.TryEnterReadLock(10);
            rwLock.TryEnterReadLock(50);
            rwLock.ExitReadLock();
        }


        [TestMethod, Timeout(500)]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void TestTryEnterReadLockObjectDisposed()
        {
            RWLock rwLock = RWLockBuilder.NewLock();

            rwLock.Dispose();
            rwLock.TryEnterReadLock(10);

        }

        [TestMethod, Timeout(500)]
        public void TestTryEnterReadLock()
        {
            // These local variables are used by the GetReadLock method below.  They are accessible
            // to that method because it is nested within TestMethod2.
            int count = 2;
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();

            // Run GetReadLock() on two tasks.  Wait up to one second for count to be decremented to zero.
            Task t1 = Task.Run(() => GetReadLock());
            Task t2 = Task.Run(() => GetReadLock());
            Assert.IsTrue(SpinWait.SpinUntil(() => count == 0, 1000), "Unable to have two simultaneous readers");

            // Allow the blocked tasks to resume, which will result in their termination
            mre.Set();

            // This method is run simultaneously in two tasks
            void GetReadLock()
            {
                // Acquire a read lock
                rwLock.TryEnterReadLock(10);

                // Atomically decrement the shared count variable.  Note that merely doing count-- won't always work.
                Interlocked.Decrement(ref count);

                // Block until the main task sets the mre to true.
                mre.WaitOne();

                // Exit the read lock
                rwLock.ExitReadLock();
            }
        }


        [TestMethod, Timeout(500)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestTryEnterWriteLockNegativeInput()
        {
            // These local variables are used by the GetReadLock method below.  They are accessible
            // to that method because it is nested within TestMethod2.
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();

            rwLock.TryEnterWriteLock(-120000);
            rwLock.ExitReadLock();
        }

        [TestMethod, Timeout(500)]
        [ExpectedException(typeof(LockRecursionException))]
        public void TestTryEnterWriteLockLockRecursion()
        {
            // These local variables are used by the GetReadLock method below.  They are accessible
            // to that method because it is nested within TestMethod2.
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();

            rwLock.TryEnterWriteLock(10);
            rwLock.TryEnterWriteLock(50);
            rwLock.ExitReadLock();
        }


        [TestMethod, Timeout(500)]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void TestTryEnterWriteLockObjectDisposed()
        {
            RWLock rwLock = RWLockBuilder.NewLock();

            rwLock.Dispose();
            rwLock.TryEnterWriteLock(10);

        }

        [TestMethod, Timeout(500)]
        public void TestTryEnterWriteLock()
        {
            // These local variables are used by the GetReadLock method below.  They are accessible
            // to that method because it is nested within TestMethod2.
            int count = 2;
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();

            // Run GetWriteLock() on two tasks.  Wait up to one second for count to be decremented to zero.
            Task t1 = Task.Run(() => GetWriteLock());
            Task t2 = Task.Run(() => GetWriteLock());
            Assert.IsTrue(SpinWait.SpinUntil(() => count == 0, 1000), "Unable to have two simultaneous readers");

            // Allow the blocked tasks to resume, which will result in their termination
            mre.Set();

            // This method is run simultaneously in two tasks
            void GetWriteLock()
            {
                // Acquire a write lock
                rwLock.TryEnterWriteLock(10);

                // Atomically decrement the shared count variable.  Note that merely doing count-- won't always work.
                Interlocked.Decrement(ref count);

                // Block until the main task sets the mre to true.
                mre.WaitOne();

                // Exit the read lock
                rwLock.ExitWriteLock();
            }
        }

        [TestMethod, Timeout(500)]
        public void TestIsReadLockHeldFalse()
        {
            RWLock rw = RWLockBuilder.NewLock();
            Assert.IsFalse(rw.IsReadLockHeld);
        }

        [TestMethod, Timeout(500)]
        public void TestIsWriteLockHeldFalse()
        {
            RWLock rw = RWLockBuilder.NewLock();
            Assert.IsFalse(rw.IsWriteLockHeld);
        }

        [TestMethod, Timeout(500)]
        public void TestIsReadLockHeldTrue()
        {
            RWLock rw = RWLockBuilder.NewLock();
            rw.EnterReadLock();
            Assert.IsTrue(rw.IsReadLockHeld);
            rw.ExitReadLock();
            Assert.IsFalse(rw.IsReadLockHeld);
        }

        [TestMethod, Timeout(500)]
        public void TestIsWriteLockHeldTrue()
        {
            RWLock rw = RWLockBuilder.NewLock();
            rw.EnterWriteLock();
            Assert.IsTrue(rw.IsWriteLockHeld);
            rw.ExitWriteLock();
            Assert.IsFalse(rw.IsWriteLockHeld);
        }

        [TestMethod, Timeout(500)]
        public void TestTryEnterWriteLockTrue()
        {
            RWLock rw = RWLockBuilder.NewLock();
            Assert.IsTrue(rw.TryEnterWriteLock(0));
        }

        [TestMethod, Timeout(500)]
        public void TestTryEnterWriteLockFalse()
        {
            bool InWriteLock = false;
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rw = RWLockBuilder.NewLock();
            Task t1 = Task.Run(() => TryEnterWriteLockNotAllowed());
            SpinWait.SpinUntil(() => InWriteLock, 100);
            Assert.IsFalse(rw.TryEnterWriteLock(0));

            mre.Set();

            void TryEnterWriteLockNotAllowed()
            {
                rw.EnterWriteLock();

                InWriteLock = true;

                mre.WaitOne();

                rw.ExitWriteLock();
            }
        }

        [TestMethod, Timeout(500)]
        public void TestTryEnterReadLockTrue()
        {
            RWLock rw = RWLockBuilder.NewLock();
            Assert.IsTrue(rw.TryEnterReadLock(0));
        }

        [TestMethod]
        public void TestTryEnterReadLockFalse()
        {
            bool InWriteLock = false;
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rw = RWLockBuilder.NewLock();
            Task t1 = Task.Run(() => TryEnterWriteLockNotAllowed());
            SpinWait.SpinUntil(() => InWriteLock, 100);
            Assert.IsFalse(rw.TryEnterReadLock(0));

            mre.Set();

            void TryEnterWriteLockNotAllowed()
            {
                rw.EnterWriteLock();

                InWriteLock = true;

                mre.WaitOne();

                rw.ExitWriteLock();
            }
        }

    }
}
