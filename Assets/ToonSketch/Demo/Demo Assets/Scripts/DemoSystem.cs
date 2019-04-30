using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToonSketch.Demo
{
    public class DemoSystem : MonoBehaviour
    {
        private enum AnimationState
        {
            Idle,
            Walk,
            Run,
            Salute
        }

        public DemoCamera mainCam;
        public ToonSketchPostProcessing camEffects;
        public Animator charAnim;
        public MeshRenderer[] meshRenderers;
        public SkinnedMeshRenderer[] skinRenderers;
        public DemoOrbit[] spotLights;
        public Light pointLight;
		public Texture2D[] rampTextures;
		public PostProcessProfile[] effectProfiles;
        public bool hideGUI = false;

        private List<Material> materials;
        private bool spotLightsOn;
        private bool spotLightsOrbit;
        private bool pointLightOn;
        private bool hatchingOn;
        private int currentRamp;
		private bool softRamp;
		private int currentProfile;

        private void Awake()
        {
            CacheMaterials();
            SetAnimation(AnimationState.Idle);
            SetOrbitCam();
            SetSpotLights(true);
            SetSpotLightsOrbit(true);
            SetPointLight(false);
            SetHatching(true);
			SetRampTexture(0);
			SetSoftRamp(true);
			SetEffectProfile(0);
        }

        private void CacheMaterials()
        {
            materials = new List<Material>();
            foreach (MeshRenderer renderer in meshRenderers)
                foreach (Material material in renderer.materials)
                    materials.Add(material);
            foreach (SkinnedMeshRenderer renderer in skinRenderers)
                foreach (Material material in renderer.materials)
                    materials.Add(material);
        }

        private void SetAnimation(AnimationState value)
        {
            switch (value)
            {
                case AnimationState.Idle:
                    charAnim.SetFloat("speed", 0f);
                    charAnim.SetBool("salute", false);
                    break;
                case AnimationState.Walk:
                    charAnim.SetFloat("speed", 0.5f);
                    charAnim.SetBool("salute", false);
                    break;
                case AnimationState.Run:
                    charAnim.SetFloat("speed", 1.5f);
                    charAnim.SetBool("salute", false);
                    break;
                case AnimationState.Salute:
                    charAnim.SetFloat("speed", 0f);
                    charAnim.SetBool("salute", true);
                    break;
            }
        }

        private void SetOrbitCam()
        {
            mainCam.gameObject.SetActive(true);
            mainCam.orbitSpeed = 20f;
            mainCam.autoOrbit = true;
        }

        private void SetStaticCam()
        {
            mainCam.gameObject.SetActive(true);
            mainCam.orbitSpeed = 0f;
            mainCam.autoOrbit = true;
        }

        private void SetFreeCam()
        {
            mainCam.gameObject.SetActive(true);
            mainCam.orbitSpeed = 20f;
            mainCam.autoOrbit = false;
        }

        private void SetSpotLights(bool value)
        {
            spotLightsOn = value;
            foreach (DemoOrbit light in spotLights)
                light.gameObject.SetActive(spotLightsOn);
        }

        private void SetSpotLightsOrbit(bool value)
        {
            spotLightsOrbit = value;
            foreach (DemoOrbit light in spotLights)
                light.orbiting = spotLightsOrbit;
        }

        private void SetPointLight(bool value)
        {
            pointLightOn = value;
            pointLight.gameObject.SetActive(pointLightOn);
        }

        private void SetHatching(bool value)
        {
            hatchingOn = value;
            foreach (Material material in materials)
            {
                material.SetFloat("_HatchShading", (hatchingOn) ? 1f : 0f);
                if (hatchingOn)
                    material.EnableKeyword("_HATCH_ON");
                else
                    material.DisableKeyword("_HATCH_ON");
            }
        }

		private void SetRampTexture(int value)
		{
			if (rampTextures.Length == 0)
				return;
			currentRamp = value % rampTextures.Length;
			foreach (Material material in materials)
				material.SetTexture("_RampTex", rampTextures[currentRamp]);
		}

		private void SetSoftRamp(bool value)
		{
			softRamp = value;
			foreach (Material material in materials)
				material.SetFloat("_RampStyle", (softRamp) ? 0 : 1);
		}

		private void SetEffectProfile(int value)
		{
			if (effectProfiles.Length == 0)
				return;
			currentProfile = value % effectProfiles.Length;
			camEffects.profile = effectProfiles[currentProfile];
		}

        private void OnGUI()
        {
            if (hideGUI)
                return;
            int width = 200;
            int x = 10;
            int y = 10;
            // Animations
            GUI.Box(new Rect(x, y, width, 120), "Animation");
            x += 10;
            y += 30;
            if (GUI.Button(new Rect(x, y, width - 20, 20), "Idle"))
            {
                SetAnimation(AnimationState.Idle);
            }
            y += 20;
            if (GUI.Button(new Rect(x, y, width - 20, 20), "Walk"))
            {
                SetAnimation(AnimationState.Walk);
            }
            y += 20;
            if (GUI.Button(new Rect(x, y, width - 20, 20), "Run"))
            {
                SetAnimation(AnimationState.Run);
            }
            y += 20;
            if (GUI.Button(new Rect(x, y, width - 20, 20), "Salute"))
            {
                SetAnimation(AnimationState.Salute);
            }
            x -= 10;
            y += 40;
            // Cameras
            GUI.Box(new Rect(x, y, width, 100), "Camera");
            x += 10;
            y += 30;
            if (GUI.Button(new Rect(x, y, width - 20, 20), "Orbit Cam"))
            {
                SetOrbitCam();
            }
            y += 20;
            if (GUI.Button(new Rect(x, y, width - 20, 20), "Static Cam"))
            {
                SetStaticCam();
            }
            y += 20;
            if (GUI.Button(new Rect(x, y, width - 20, 20), "Free Cam"))
            {
                SetFreeCam();
            }
            x -= 10;
            y += 40;
            // Lights
            GUI.Box(new Rect(x, y, width, 100), "Lights");
            x += 10;
            y += 30;
            if (GUI.Button(new Rect(x, y, width - 20, 20), "Toggle Spotlights"))
            {
                SetSpotLights(!spotLightsOn);
            }
            y += 20;
            if (GUI.Button(new Rect(x, y, width - 20, 20), "Toggle Spotlight Orbit"))
            {
                SetSpotLightsOrbit(!spotLightsOrbit);
            }
            y += 20;
            if (GUI.Button(new Rect(x, y, width - 20, 20), "Toggle Point Light"))
            {
                SetPointLight(!pointLightOn);
            }
            x -= 10;
            y += 40;
            // Materials
            GUI.Box(new Rect(x, y, width, 100), "Materials");
            x += 10;
            y += 30;
            if (GUI.Button(new Rect(x, y, width - 20, 20), "Toggle Hatching"))
            {
                SetHatching(!hatchingOn);
            }
            y += 20;
            if (GUI.Button(new Rect(x, y, width - 20, 20), "Switch Ramp Texture"))
            {
				SetRampTexture(currentRamp + 1);
            }
            y += 20;
            if (GUI.Button(new Rect(x, y, width - 20, 20), "Ramp Style: " + ((softRamp) ? "Soft" : "Hard")))
            {
				SetSoftRamp(!softRamp);
            }
            x -= 10;
            y += 40;
            // Effects
            GUI.Box(new Rect(x, y, width, 60), "Effects");
            x += 10;
            y += 30;
            if (GUI.Button(new Rect(x, y, width - 20, 20), "Switch Effects Profile"))
            {
				SetEffectProfile(currentProfile + 1);
            }
        }
    }
}