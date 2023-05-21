using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
using CorkUtil;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CorkCore
{
    [System.Serializable]
    public class PlayerDataModel
    {
        private const string PlayerprefsName = "playerCustomData";

        bool IsValid => name != "" && pronouns != "" && meshIndex != -1;

        public string name;
        public string pronouns;
        public int meshIndex = 0;

        public PlayerDataModel(string _name, string _pronouns)
        {
            name = _name;
            pronouns = _pronouns;
        }

        public bool TrySave()
        {
            if (!IsValid)
            {
                Debug.Log("Not ready to save");
                return false;
            }

            string json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(PlayerprefsName, json);
            Debug.Log("Saved!");
            return true;
        }


        public static PlayerDataModel Load()
        {
            PlayerDataModel val = null;
            if (PlayerPrefs.GetString("playerCustomData", String.Empty) == String.Empty)
            {
                Debug.Log("Player Data Model not found, creating new one");
                val = new PlayerDataModel("", "");
            }
            else
                val = JsonUtility.FromJson<PlayerDataModel>(PlayerPrefs.GetString(PlayerprefsName));

            return val;
        }
    }

    public class CharacterCreation : MonoBehaviour
    {
        public bool forceCreation = true;
        [Header("Scene References")] [SerializeField]
        private MeshFilter mesh = null;

        [SerializeField] private GameObject player = null;
        [SerializeField] private Animator controller = null;
        [SerializeField] private EnableComponents enableComponents = null;
        [SerializeField] private CharacterController characterController = null;
        [SerializeField] private TMPro.TMP_InputField nameField = null;
        [SerializeField] private TMPro.TMP_Dropdown pronounField = null;
        [SerializeField] private Button doneButton = null;
        [SerializeField] private List<MeshFilter> meshes = new List<MeshFilter>();
        [SerializeField] private Button leftMeshButton = null;
        [SerializeField] private Button rightMeshButton = null;

        [Header("Events")] public UnityEvent OnFinishedCreation;
        // Start is called before the first frame update

        private PlayerDataModel _dataModel;

        private int _meshIndex = 0;

        private void Awake()
        {
            if (TryLoad())
                return;
            doneButton.gameObject.SetActive(false);

            #region Data

            nameField.onEndEdit.AddListener(x =>
            {
                _dataModel.name = x;
                TrySave();
            });
            pronounField.onValueChanged.AddListener(x =>
            {
                _dataModel.pronouns = pronounField.options[x].text;
                TrySave();
            });

            #endregion

            #region Mesh

            leftMeshButton.onClick.AddListener(() =>
            {
                if (_meshIndex - 1 < 0)
                    _meshIndex = meshes.Count - 1;
                _meshIndex--;
                UpdateMesh();
                TrySave();
            });
            rightMeshButton.onClick.AddListener(() =>
            {
                _meshIndex = (_meshIndex + 1) % meshes.Count;
                UpdateMesh();
                TrySave();
            });

            #endregion

            doneButton.onClick.AddListener(EndCharacterCreation);
        }

        void TrySave()
        {
            doneButton.gameObject.SetActive(_dataModel.TrySave());
        }

        public void ResetCamera()
        {
            player.transform.localPosition = new Vector3(0.0f, 0.8f, 0.0f);
            player.transform.localRotation = Quaternion.identity;
            enableComponents.EnableAllComponents();
            characterController.enabled = true;
        }

        public void UpdateMesh()
        {
            _dataModel.meshIndex = _meshIndex;
            mesh.mesh = meshes[_meshIndex].mesh;
        }

        public void EndCharacterCreation()
        {
            controller.SetBool("Done", true);
            _dataModel.TrySave();
            controller.enabled = false;
            ResetCamera();
            OnFinishedCreation.Invoke();
            gameObject.SetActive(false);
        }

        public bool TryLoad()
        {
            _dataModel = PlayerDataModel.Load();
            if (forceCreation || _dataModel.name == string.Empty)
                return false;
            mesh.mesh = meshes[_dataModel.meshIndex].mesh;
            EndCharacterCreation();
            return true;
        }
    }
}