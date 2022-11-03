using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pie.Math
{
    public class MyMaths
    {
        /// <summary>
        /// Returns a random index from an array, based on weights
        /// </summary>
        /// <param name="newWeights"></param>
        /// <returns></returns>
        public static int GetRandomWeightedIndex(List<int> newWeights)
        {
            List<int> weights = new List<int>();
            for (int i = 0; i < newWeights.Count; i++)
            {
                weights.Add(newWeights[i]);
            }

            // Get the total sum of all the weights.
            int weightSum = 0;
            for (int i = 0; i < weights.Count; ++i)
            {
                weightSum += weights[i];
            }

            // Step through all the possibilities, one by one, checking to see if each one is selected.
            int index = 0;
            int lastIndex = weights.Count - 1;

            while (index < lastIndex)
            {
                // Do a probability check with a likelihood of weights[index] / weightSum.
                if (Random.Range(0, weightSum) < weights[index])
                {
                    return index;
                }

                // Remove the last item from the sum of total untested weights and try again.
                weightSum -= weights[index];
                index++;
            }

            // No other item was selected, so return very last index.
            return index;
        }

        /// <summary>
        /// Returns an int from an array, based on weights
        /// </summary>
        /// <param name="newWeights"></param>
        /// <returns></returns>
        public static int GetRandomWeightedIndex(List<float> newWeights)
        {
            List<float> weights = new List<float>();
            for (int i = 0; i < newWeights.Count; i++)
            {
                weights.Add(newWeights[i]);
            }

            // Get the total sum of all the weights.
            float weightSum = 0;
            for (int i = 0; i < weights.Count; ++i)
            {
                weightSum += weights[i];
            }

            // Step through all the possibilities, one by one, checking to see if each one is selected.
            int index = 0;
            int lastIndex = weights.Count - 1;

            while (index < lastIndex)
            {
                // Do a probability check with a likelihood of weights[index] / weightSum.
                if (Random.Range(0, weightSum) <= weights[index] && weights[index] > 0)
                {
                    return index;
                }

                // Remove the last item from the sum of total untested weights and try again.
                weightSum -= weights[index];
                index++;
            }

            // No other item was selected, so return very last index.
            return index;
        }

        /* List shuffle
        /// <summary>
        /// Shuffles a list of TMP_Text
        /// </summary>
        /// <param name="listToShuffle"></param>
        /// <returns></returns>
        public static List<TMPro.TMP_Text> ListShuffler(List<TMPro.TMP_Text> listToShuffle)
        {
            for (int i = 0; i < listToShuffle.Count; i++)
            {
                TMPro.TMP_Text temp = listToShuffle[i];
                int randomIndex = Random.Range(i, listToShuffle.Count);
                listToShuffle[i] = listToShuffle[randomIndex];
                listToShuffle[randomIndex] = temp;
            }

            return listToShuffle;
        }
        */

        public static Quaternion rotationFromPoints(List<Transform> places, Transform referencePlace)
        {
            Quaternion targetRotation;

            // Calculate average path, which will give y height at 1 x
            float sumX = 0;
            float sumY = 0;
            float SSx = 0;
            float SP = 0;

            // Gets the sum of the places
            foreach (Transform quickTrans in places)
            {
                sumX += quickTrans.position.x;
                sumY += quickTrans.position.y;
            }

            // Gets the means of the places
            float meanX = sumX / (float)places.Count;
            float meanY = sumY / (float)places.Count;

            // Gets the Sum of Squares and Sum of Products
            foreach (Transform quickTrans in places)
            {
                SSx += (quickTrans.position.x - meanX) * (quickTrans.position.x - meanX);

                SP += (quickTrans.position.x - meanX) * (quickTrans.position.y - meanY);
            }

            // Figures out the vector
            float lineDirection = SP/SSx;

            bool isBelow = true;
            bool isAbove = true;

            // If all legs are below y of the body, have the body always point more upwards
            foreach (Transform quickTrans in places)
            {
                if (quickTrans.position.y > referencePlace.position.y)
                {
                    isBelow = false;
                }
                else if (quickTrans.position.y < referencePlace.position.y)
                {
                    isAbove = false;
                }
            }

            float secondDirection = 1f;

            if (isBelow)
            {
                if (lineDirection > 0f)
                {
                    secondDirection = -1f;
                }
                lineDirection = -Mathf.Abs(lineDirection);
            }
            else if (isAbove)
            {
                if (lineDirection < 0f)
                {
                    secondDirection = -1f;
                }
                lineDirection = Mathf.Abs(lineDirection);
            }

            // Translates this direction to a rotation
            targetRotation = Quaternion.LookRotation(Vector3.forward, new Vector3(secondDirection, -1f / lineDirection, 0f));

            return targetRotation;
        }
    }
}
