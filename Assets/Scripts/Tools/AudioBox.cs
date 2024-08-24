using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;



namespace UsefulBox
{
    [System.Serializable]
    public struct FMODParam
    {
        public string parameterName;
        public float parameterValue;
        public FMODParam(string name, float value)
        {
            parameterName = name;
            parameterValue = value;
        }
    }

    public static class AudioBox
    {
        public static void PlayOneShot(FMOD.GUID guid, FMODParam[] parameters, Vector3 position = new Vector3())
        {
            var instance = RuntimeManager.CreateInstance(guid);
            instance.set3DAttributes(RuntimeUtils.To3DAttributes(position));
            if (parameters != null)
            {
                foreach (FMODParam param in parameters)
                {
                    instance.setParameterByName(param.parameterName, param.parameterValue);
                }
            }

            instance.start();
            instance.release();
        }
        public static void PlayOneShot(string path, FMODParam[] parameters, Vector3 position = new Vector3())
        {
            try
            {
                PlayOneShot(RuntimeManager.PathToGUID(path), parameters, position);
            }
            catch (EventNotFoundException)
            {
                RuntimeUtils.DebugLogWarning("[FMOD] Event not found: " + path);
            }
        }
        public static void PlayOneShot(EventReference eventReference, FMODParam[] parameters, Vector3 position = new Vector3())
        {
            try
            {
                PlayOneShot(eventReference.Guid, parameters, position);
            }
            catch (EventNotFoundException)
            {
                RuntimeUtils.DebugLogWarning("[FMOD] Event not found: " + eventReference);
            }
        }




    }


}

