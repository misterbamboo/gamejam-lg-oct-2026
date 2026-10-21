using System.Collections.Generic;
using UnityEngine;

// The peppers available right now. Each one taken is gone; once empty, a new batch is generated.
public class PimentBatch
{
    private readonly Stack<PimentData> piments = new Stack<PimentData>();
    private readonly int batchSize;

    public PimentBatch(int batchSize)
    {
        this.batchSize = Mathf.Max(1, batchSize);
        Generate();
    }

    public int Remaining => piments.Count;

    public PimentData Take()
    {
        if (piments.Count == 0)
        {
            Generate();
        }

        return piments.Pop();
    }

    private void Generate()
    {
        for (int i = 0; i < batchSize; i++)
        {
            piments.Push(PimentData.Random());
        }
    }
}
