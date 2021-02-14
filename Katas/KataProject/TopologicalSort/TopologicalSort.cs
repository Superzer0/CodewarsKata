using System;
using System.Collections.Generic;
using System.Linq;

namespace TopologicalSort
{
    public class TopologicalSort
    {
        public IEnumerable<IStartable> Sort(IEnumerable<IStartable> elementsToSort)
        {
            if(elementsToSort == null)
            {
                throw new ArgumentNullException(nameof(elementsToSort));
            }

            if(!elementsToSort.Any())
            {
                return elementsToSort;
            }

            IList<(string type, IStartable obj)> nodes = elementsToSort.Select(p => (p.GetType().Name, p)).ToList();
            
            var edges = new List<Edge>();
            foreach (var itemType in elementsToSort.Select(p => p.GetType()))
            {
                foreach (var dependsOnAttr in (DependsOnAttribute[])Attribute.GetCustomAttributes(itemType, 
                        typeof(DependsOnAttribute)))
                {
                    edges.Add(new Edge {
                          From = dependsOnAttr.DependsOn.Name,
                          To = itemType.Name
                    });
                }
            }

            var result = new List<IStartable>();

            while(nodes.Any())
            {
                var nodeToRemove = nodes.First(n => !edges.Any(e => e.To == n.type));
                nodes.Remove(nodeToRemove);
                edges = edges.Where(e => e.From != nodeToRemove.type).ToList();
                result.Add(nodeToRemove.obj);
            }

            return result;
        }

        private class Edge
        {
            public string From { get; set; }
            public string To { get; set; }
        }

    }
}
