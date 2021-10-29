using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleNeuralNet {

    private List<float[,]> all_weights;
    private List<float[ ]> all_results;

    public SimpleNeuralNet(SimpleNeuralNet other) {
        all_weights = new List<float[,]>();
        all_results = new List<float[ ]>();
        for (int i = 0; i < other.all_weights.Count; i++) {
            all_weights.Add((float[,]) other.all_weights[i].Clone());
            all_results.Add((float[ ]) other.all_results[i].Clone());
        }
    }

    public SimpleNeuralNet(int[] structure) {
        all_weights = new List<float[,]>();
        all_results = new List<float[ ]>();
        for (int i = 1; i < structure.Length; i++) {
            float[,] weights = makeLayer(structure[i-1], structure[i]);
            all_weights.Add(weights);
            float[] results = new float[structure[i]];
            all_results.Add(results);
        }
    }

    private float[,] makeLayer(int input, int nb_nodes) {
        // weights: bias+input x neurons
        float[,] weights = new float[input + 1, nb_nodes];
        for (int i = 0; i < weights.GetLength(0); i++) {
            for (int j = 0; j < weights.GetLength(1); j++) {
                weights[i, j] = (2.0f * UnityEngine.Random.value - 1.0f) * 10.0f;
            }
        }
        return weights;
    }

    public float[] getOutput(float[] input) {
        for (int layer_i = 0; layer_i < all_weights.Count; layer_i++) {
            float[,] weights = all_weights[layer_i];
            float[] ins = input;
            float[] outs = all_results[layer_i];
            if (layer_i > 0)
                ins = all_results[layer_i - 1];

            for (int neuron_i = 0; neuron_i < outs.Length; neuron_i++) {
                float sum = weights[0, neuron_i]; // Add bias
                for (int input_i = 0; input_i < ins.Length; input_i++) {
                    sum += ins[input_i] * weights[input_i+1, neuron_i];
                }
                outs[neuron_i] = transferFunction(sum); // Apply transfer function
            }
        }
        return all_results[all_results.Count - 1]; // Return final result
    }

    private float transferFunction(float value) {
        return 1.0f / (1.0f + Mathf.Exp(-value));
    }

    // Randomly change network weights
    // Swap: completely change a weight to a value between [-1;1]*swap_strength
    // Eps: change a weight by adding a value between [-1;1]*eps_strength
    public void mutate(float swap_rate, float eps_rate, float swap_strength, float eps_strength) {
        foreach (float[,] weights in all_weights) {
            for (int i = 0; i < weights.GetLength(0); i++) {
                for (int j = 0; j < weights.GetLength(1); j++) {
                    float rand = UnityEngine.Random.value;
                    if (rand < swap_rate) {
                        weights[i, j] = (2.0f * UnityEngine.Random.value - 1.0f) * swap_strength;
                    } else if (rand < swap_rate + eps_rate) {
                        weights[i, j] += (2.0f * UnityEngine.Random.value - 1.0f) * eps_strength;
                    }
                }
            }
        }
    }

}
