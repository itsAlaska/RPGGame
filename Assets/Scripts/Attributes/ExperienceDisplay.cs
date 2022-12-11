using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;

        void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        void Update()
        {
            GetComponent<TMP_Text>().text = $"{experience.GetPoints()}";
        }
    }
}
