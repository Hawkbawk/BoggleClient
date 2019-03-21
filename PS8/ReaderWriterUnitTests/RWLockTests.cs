// Initial version written by Joseph Zachary for CS 3500
// Copyright Joseph Zachary, March 2019

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReaderWriterLockClasses;
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
        public void TestMethod1()
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
        [TestMethod, Timeout(1500)]
        public void TestMethod2()
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
        /// Verifies that two tasks can simultaneously acquire the same read lock... 
        /// 
        /// TESTS isReadlockHeld method to check if method actually works
        /// </summary>
        [TestMethod, Timeout(500)]
        public void TestMethod3()
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
                    Interlocked.Increment(ref EnterReadLockCount);
                }
                // Atomically decrement the shared count variable.  Note that merely doing count-- won't always work.
                Interlocked.Decrement(ref count);
               

                // Block until the main task sets the mre to true.
                mre.WaitOne();

                // Exit the read lock
                rwLock.ExitReadLock();

                if (!rwLock.IsReadLockHeld)     //check if read lock is currently off
                {
                    Interlocked.Increment(ref ExitReadLockCount);
                }
            }
        }

        /// <summary>
        /// Verifies that two tasks can simultaneously acquire the same read lock... 
        /// 
        /// TESTS GetReadCount method to check if there are actually 3 threads that have entered the lock in read mode
        /// </summary>
        [TestMethod, Timeout(200)]
        public void TestMethod4()
        {
            // These local variables are used by the GetReadLock method below.  They are accessible
            // to that method because it is nested within TestMethod2.
            int count = 3;
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();

            // Run GetReadLock() on two tasks.  Wait up to one second for count to be decremented to zero.
            Task t1 = Task.Run(() => GetReadLock());
            Task t2 = Task.Run(() => GetReadLock());
            Task t3 = Task.Run(() => GetReadLock());
            Assert.IsTrue(SpinWait.SpinUntil(() => count == 0, 1000), "Unable to have two simultaneous readers");

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
        /// Verifies that two tasks can simultaneously acquire the same read lock... 
        /// 
        /// TESTS GetReadCount method to check if there are actually 2 threads that have entered the lock in read mode
        /// </summary>
        [TestMethod, Timeout(200)]
        public void TestMethod5()
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
        /// Verifies that two tasks can simultaneously acquire the same read lock... 
        /// 
        /// TESTS WaitingWriteCount
        /// </summary>
        [TestMethod, Timeout(200)]
        public void TestMethod6()
        {
            // These local variables are used by the GetReadLock method below.  They are accessible
            // to that method because it is nested within TestMethod2.
            int count = 2;
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();

            // Run GetReadLock() on two tasks.  Wait up to one second for count to be decremented to zero.
            Task t1 = Task.Run(() => GetWriteLock());
            Task t2 = Task.Run(() => GetWriteLock());
            Assert.IsTrue(SpinWait.SpinUntil(() => count == 0, 1000), "Unable to have two simultaneous writers");

            // Allow the blocked tasks to resume, which will result in their termination
            mre.Set();

            void GetWriteLock()
            {
                rwLock.EnterWriteLock();
                Assert.IsTrue(rwLock.IsWriteLockHeld);

                Interlocked.Decrement(ref count);

                rwLock.ExitWriteLock();
                mre.WaitOne();

                Assert.IsFalse(rwLock.IsWriteLockHeld);
            }
        }

        /// <summary>
        /// Verifies that two tasks can simultaneously acquire the same read lock... 
        /// 
        /// TESTS WaitingWriteCount
        /// </summary>
        [TestMethod]
        public void TestWaitingWriteCountWorks()
        {
            // These local variables are used by the GetReadLock method below.  They are accessible
            // to that method because it is nested within TestMethod2.
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();

            // Run GetWriteLock() on two tasks. 
            Task t1 = Task.Run(() => GetWriteLock());
            Task t2 = Task.Run(() => WaitForWriteLock());
            Thread.Sleep(10);


            // Allow the blocked tasks to resume, which will result in their termination
            mre.Set();

            void GetWriteLock()
            {
                rwLock.EnterWriteLock();
                Assert.IsTrue(rwLock.IsWriteLockHeld);

                // Wait until another thread tries to enter the lock, or 1 second. 
                Assert.IsTrue(SpinWait.SpinUntil(() => rwLock.WaitingWriteCount == 1, 1000), "No thread is trying to enter write mode.");

                rwLock.ExitWriteLock();
                mre.WaitOne();

                Assert.IsFalse(rwLock.IsWriteLockHeld);
            }

            void WaitForWriteLock()
            {
                Thread.Sleep(10);
                rwLock.EnterWriteLock();
                rwLock.ExitWriteLock();
                mre.WaitOne();
            }
        }


        /// <summary>
        /// Verifies that two tasks can simultaneously acquire the same read lock... 
        /// 
        /// TESTS WaitingReadCount
        /// </summary>
        [TestMethod]
        public void TestWaitingReadCountWorks()
        {
            // These local variables are used by the GetReadLock method below.  They are accessible
            // to that method because it is nested within TestMethod2.
            ManualResetEvent mre = new ManualResetEvent(false);
            RWLock rwLock = RWLockBuilder.NewLock();

            // Run GetWriteLock() on two tasks. 
            Task t1 = Task.Run(() => GetReadLock());
            Task t2 = Task.Run(() => WaitForReadLock());

            Thread.Sleep(10);


            // Allow the blocked tasks to resume, which will result in their termination
            mre.Set();

            void GetReadLock()
            {
                rwLock.EnterReadLock();

                // Wait until another thread tries to enter the lock, or 1 second. 
                Assert.IsTrue(SpinWait.SpinUntil(() => rwLock.WaitingReadCount == 1, 1000), "No thread is trying to enter write mode.");

                rwLock.ExitReadLock();
                mre.WaitOne();

            }

            void WaitForReadLock()
            {
                Thread.Sleep(10);
                rwLock.EnterReadLock();
                rwLock.ExitReadLock();
                mre.WaitOne();
            }
        }


        [TestMethod]
        [ExpectedException(typeof(LockRecursionException))]
        public void TestMultipleEnterRead()
        {
            RWLock rw = RWLockBuilder.NewLock();
            rw.EnterReadLock();
            rw.EnterReadLock();
        }


        [TestMethod]
        [ExpectedException(typeof(LockRecursionException))]
        public void TestMultipleEnterWrite()
        {
            RWLock rw = RWLockBuilder.NewLock();
            rw.EnterWriteLock();
            rw.EnterWriteLock();
        }

        [TestMethod]
        [ExpectedException(typeof(SynchronizationLockException))]
        public void TestExitReadWithoutEnter()
        {
            RWLock rw = RWLockBuilder.NewLock();
            rw.ExitReadLock();
        }











































    }
}
