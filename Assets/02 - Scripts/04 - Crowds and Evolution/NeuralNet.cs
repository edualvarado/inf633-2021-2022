using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleNeuralNet
{

    private List<float[,]> allWeights;
    private List<float[]> allResults;

    public SimpleNeuralNet(SimpleNeuralNet other)
    {
        allWeights = new List<float[,]>();
        allResults = new List<float[]>();

        for (int i = 0; i < other.allWeights.Count; i++)
        {
            allWeights.Add((float[,])other.allWeights[i].Clone());
            allResults.Add((float[])other.allResults[i].Clone());
        }
    }

    public SimpleNeuralNet(int[] structure)
    {
        allWeights = new List<float[,]>();
        allResults = new List<float[]>();

        for (int i = 1; i < structure.Length; i++)
        {
            float[,] weights = makeLayer(structure[i - 1], structure[i]);
            allWeights.Add(weights);
            float[] results = new float[structure[i]];
            allResults.Add(results);
        }
    }

    private float[,] makeLayer(int input, int numberNodes)
    {

        // Weights: bias + input x neurons
        float[,] weights = new float[input + 1, numberNodes];
        for (int i = 0; i < weights.GetLength(0); i++)
        {
            for (int j = 0; j < weights.GetLength(1); j++)
            {
                weights[i, j] = (2.0f * UnityEngine.Random.value - 1.0f) * 10.0f;
            }
        }
        return weights;
    }

    public float[] getOutput(float[] input)
    {
        for (int idxLayer = 0; idxLayer < allWeights.Count; idxLayer++)
        {
            float[,] weights = allWeights[idxLayer];
            float[] ins = input;
            float[] outs = allResults[idxLayer];
            if (idxLayer > 0)
                ins = allResults[idxLayer - 1];

            for (int idxNeuron = 0; idxNeuron < outs.Length; idxNeuron++)
            {
                float sum = weights[0, idxNeuron]; // Add bias
                for (int input_i = 0; input_i < ins.Length; input_i++)
                {
                    sum += ins[input_i] * weights[input_i + 1, idxNeuron];
                }
                outs[idxNeuron] = transferFunction(sum); // Apply transfer function
            }
        }
        return allResults[allResults.Count - 1]; // Return final result
    }

    private float transferFunction(float value)
    {
        return 1.0f / (1.0f + Mathf.Exp(-value));
    }

    // Randomly change network weights
    // Swap: completely change a weight to a value between [-1;1]*swap_strength
    // Eps: change a weight by adding a value between [-1;1]*eps_strength
    public void mutate(float swap_rate, float eps_rate, float swap_strength, float eps_strength)
    {
        foreach (float[,] weights in allWeights)
        {
            for (int i = 0; i < weights.GetLength(0); i++)
            {
                for (int j = 0; j < weights.GetLength(1); j++)
                {
                    float rand = UnityEngine.Random.value;
                    if (rand < swap_rate)
                    {
                        weights[i, j] = (2.0f * UnityEngine.Random.value - 1.0f) * swap_strength;
                    }
                    else if (rand < swap_rate + eps_rate)
                    {
                        weights[i, j] += (2.0f * UnityEngine.Random.value - 1.0f) * eps_strength;
                    }
                }
            }
        }
    }

}
