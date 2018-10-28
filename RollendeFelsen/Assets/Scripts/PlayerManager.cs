using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;

        SkinnedMeshRenderer mskinnedMeshRenderer = player.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();

        if (mskinnedMeshRenderer != null) {
            if (EngineUnity.isFirstSkin)
                mskinnedMeshRenderer.material.color = Color.gray;
            else if (EngineUnity.isSecondSkin)
                mskinnedMeshRenderer.material.color = Color.cyan;
            else if (EngineUnity.isThirdSkin)
                mskinnedMeshRenderer.material.color = Color.yellow;
        }
    }

    #endregion

    public GameObject player;
    public GameObject target;
}
