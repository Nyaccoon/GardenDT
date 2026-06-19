using UnityEngine;
using TMPro;

public class PillarAnimalInput : MonoBehaviour
{
    [SerializeField] private PillarMath pillarMath;

    [SerializeField] private TMP_InputField insectsInput;
    [SerializeField] private TMP_InputField birdsInput;
    [SerializeField] private TMP_InputField spidersInput;
    [SerializeField] private TMP_InputField otherAnimalsInput;

    public void ApplyValues()
    {
        if (pillarMath == null) return;

        pillarMath.nrOfInsects = Parse(insectsInput);
        pillarMath.nrOfBirds = Parse(birdsInput);
        pillarMath.nrOfSpiders = Parse(spidersInput);
        pillarMath.nrOfOtherAnimals = Parse(otherAnimalsInput);

        pillarMath.Recalculate();  
    }

    private int Parse(TMP_InputField input)
    {
        if (input == null) return 0;

        if (int.TryParse(input.text, out int value))
            return Mathf.Max(0, value);

        return 0;
    }
}
