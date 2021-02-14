using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TopologicalSort.Tests
{
    [TestFixture]
    public class TopologicalSortTests
    {
        private TopologicalSort CreateSut()
        {
            return new TopologicalSort();
        }

        [Test]
        public void NoElements_EmptyListReturned()
        {
            var sut = CreateSut();
            
            var results = sut.Sort(Enumerable.Empty<IStartable>());

            CollectionAssert.IsEmpty(results);
        }
        
        [Test]
        public void NullAsArgument_ExceptionThrown()
        {
            var sut = CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.Sort(null));
        }

        [Test]
        [TestCaseSource(nameof(StrictOrderTestCases))]
        public void StrictOrder_ProperReturned(IEnumerable<IStartable> input, IEnumerable<IStartable> expectedOutput)
        {
            var sut = CreateSut();
            
            var result = sut.Sort(input);

            CollectionAssert.AreEqual(expectedOutput.Select(p => p.GetType()),
             result.Select(p => p.GetType()));
        }

        public static object[] StrictOrderTestCases => new object []{

            new object[] { 
                new List<IStartable> {
                    new UpdatePasswordsStartable(),
                    new FilesCleanupStartable(),
                    new DbMigrationStartable(),
                    new DbStatsStartable(),
                    
                },
                new List<IStartable> {
                    new UpdatePasswordsStartable(),
                    new DbMigrationStartable(),
                    new DbStatsStartable(),
                    new FilesCleanupStartable(),            
                }
            },

            new object[] { 
                new List<IStartable> {
                    new FilesCleanupStartable(),
                    new DbMigrationStartable(),
                    new DbStatsStartable()
                },
                new List<IStartable> {
                    new DbMigrationStartable(),
                    new DbStatsStartable(),
                    new FilesCleanupStartable(),            
                }
            },
        };

    }

    internal class DbMigrationStartable : IStartable
    {
        public void Run()
        {
            throw new NotImplementedException();
        }
    }

    [DependsOn(typeof(DbMigrationStartable))]
    internal class DbStatsStartable : IStartable
    {
        public void Run()
        {
            throw new NotImplementedException();
        }
    }

    [DependsOn(typeof(DbStatsStartable))]
    internal class FilesCleanupStartable : IStartable
    {
        public void Run()
        {
            throw new NotImplementedException();
        }
    }

    internal class UpdatePasswordsStartable : IStartable
    {
        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}