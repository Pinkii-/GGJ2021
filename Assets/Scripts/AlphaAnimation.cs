using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class AlphaAnimation : MonoBehaviour
{
    public AnimationCurve m_AlphaCurve = new AnimationCurve();
    public float m_AnimationTime = 1;
    
    private float m_ElapsedTime;

    private CanvasGroup m_Target;
    
    private void Awake()
    {
        m_Target = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        m_ElapsedTime = 0;
    }

    public void Update()
    {
        m_ElapsedTime += Time.deltaTime;

        m_Target.alpha = m_AlphaCurve.Evaluate(m_ElapsedTime / m_AnimationTime);
    }
}
