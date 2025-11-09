using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = System.Random;

public class AudioManager : Singleton<AudioManager>
{
    public AudioMixer mixer;
    public AudioSource ambiance, loop, sfx;

    // Ambiances
    public AudioClip amb_20_loop, amb_70_loop, amb_futur_loop;
    
    // Loops
    public AudioClip sfx_tv_loop, sfx_radio_loop;
    
    // SFX
    public AudioClip sfx_clic_possession;
    public AudioClip sfx_porte_piv_01, sfx_porte_piv_02, sfx_porte_piv_03;
    public AudioClip sfx_fenetre_piv_01, sfx_fenetre_piv_02, sfx_fenetre_piv_03;
    public AudioClip sfx_lampe_hit_01, sfx_lampe_hit_02, sfx_lampe_hit_03;
    public AudioClip sfx_coussin_hit_01, sfx_coussin_hit_02, sfx_coussin_hit_03;
    public AudioClip sfx_tableau_hit_01, sfx_tableau_hit_02, sfx_tableau_hit_03;
    public AudioClip sfx_radio_hit_01, sfx_radio_hit_02, sfx_radio_hit_03;
    public AudioClip sfx_valise_hit_01, sfx_valise_hit_02, sfx_valise_hit_03;
    public AudioClip sfx_plante_hit_01, sfx_plante_hit_02, sfx_plante_hit_03;
    public AudioClip sfx_tel_hit_01, sfx_tel_hit_02, sfx_tel_hit_03;
    public AudioClip sfx_porte_placard_hit_01, sfx_porte_placard_hit_02, sfx_porte_placard_hit_03;
    public AudioClip sfx_tiroir_hit_01, sfx_tiroir_hit_02, sfx_tiroir_hit_03;
    public AudioClip sfx_petit_objet_hit_01, sfx_petit_objet_hit_02, sfx_petit_objet_hit_03;
    public AudioClip sfx_tv_on, sfx_radio_on;
    
    // Lists
    private List<AudioClip> porte_list = new List<AudioClip>();
    private List<AudioClip> fenetre_list = new List<AudioClip>();
    private List<AudioClip> lampe_list = new List<AudioClip>();
    private List<AudioClip> coussin_list = new List<AudioClip>();
    private List<AudioClip> tableau_list = new List<AudioClip>();
    private List<AudioClip> radio_list = new List<AudioClip>();
    private List<AudioClip> valise_list = new List<AudioClip>();
    private List<AudioClip> plante_list = new List<AudioClip>();
    private List<AudioClip> tel_list = new List<AudioClip>();
    private List<AudioClip> porte_placard_list = new List<AudioClip>();
    private List<AudioClip> tiroir_list = new List<AudioClip>();
    private List<AudioClip> petit_objet_list = new List<AudioClip>();
    
    public enum Ambiance
    {
        Amb_20,
        Amb_70,
        Amb_Futur
    }

    public enum Loop
    {
        TV_Loop,
        Radio_Loop
    }
    public enum SFX
    {
        Clic,
        Porte, 
        Fenetre,
        Lampe, 
        Coussin,
        Tableau,
        Radio,
        Valise,
        Plante,
        Tel,
        Placard,
        Tiroir,
        Petit_Objet,
        TV_ON,
        Radio_ON,
    }

    private void Awake()
    {
        DontDestroyOnLoad(instance);
        
        porte_list.Add(sfx_porte_piv_01);
        porte_list.Add(sfx_porte_piv_02);
        porte_list.Add(sfx_porte_piv_03);
        
        fenetre_list.Add(sfx_fenetre_piv_01);
        fenetre_list.Add(sfx_fenetre_piv_02);
        fenetre_list.Add(sfx_fenetre_piv_03);
        
        lampe_list.Add(sfx_lampe_hit_01);
        lampe_list.Add(sfx_lampe_hit_02);
        lampe_list.Add(sfx_lampe_hit_03);
        
        coussin_list.Add(sfx_coussin_hit_01);
        coussin_list.Add(sfx_coussin_hit_02);
        coussin_list.Add(sfx_coussin_hit_03);
        
        tableau_list.Add(sfx_tableau_hit_01);
        tableau_list.Add(sfx_tableau_hit_02);
        tableau_list.Add(sfx_tableau_hit_03);
        
        radio_list.Add(sfx_radio_hit_01);
        radio_list.Add(sfx_radio_hit_02);
        radio_list.Add(sfx_radio_hit_03);
        
        valise_list.Add(sfx_valise_hit_01);
        valise_list.Add(sfx_valise_hit_02);
        valise_list.Add(sfx_valise_hit_03);
        
        plante_list.Add(sfx_plante_hit_01);
        plante_list.Add(sfx_plante_hit_02);
        plante_list.Add(sfx_plante_hit_03);
        
        tel_list.Add(sfx_tel_hit_01);
        tel_list.Add(sfx_tel_hit_02);
        tel_list.Add(sfx_tel_hit_03);
        
        porte_placard_list.Add(sfx_porte_placard_hit_01);
        porte_placard_list.Add(sfx_porte_placard_hit_02);
        porte_placard_list.Add(sfx_porte_placard_hit_03);
        
        tiroir_list.Add(sfx_tiroir_hit_01);
        tiroir_list.Add(sfx_tiroir_hit_02);
        tiroir_list.Add(sfx_tiroir_hit_03);
        
        petit_objet_list.Add(sfx_petit_objet_hit_01);
        petit_objet_list.Add(sfx_petit_objet_hit_02);
        petit_objet_list.Add(sfx_petit_objet_hit_03);
    }

    // Start is called before the first frame update
    void Start()
    {
        ambiance.clip = amb_70_loop;
        ambiance.loop = true;
        ambiance.Play();
    }

    public void PlayAmbiance(Ambiance new_Ambiance)
    {
        switch (new_Ambiance)
        {
            case Ambiance.Amb_20:
                if (ambiance.clip != amb_20_loop)
                {
                    ambiance.clip = amb_20_loop;
                    ambiance.Play();
                }
                break;
            
            case Ambiance.Amb_70:
                if (ambiance.clip != amb_70_loop)
                {
                    ambiance.clip = amb_70_loop;
                    ambiance.Play();
                }
                break;
            
            case Ambiance.Amb_Futur:
                if (ambiance.clip != amb_futur_loop)
                {
                    ambiance.clip = amb_futur_loop;
                    ambiance.Play();
                }
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(new_Ambiance), new_Ambiance, null);
        }
        
    }

    public void PlayLoop(Loop new_Loop)
    {
        switch (new_Loop)
        {
            case Loop.TV_Loop:
                if(loop.clip == sfx_tv_loop && loop.isPlaying)
                    loop.Stop();
                else
                {
                    loop.clip = sfx_tv_loop;
                    loop.Play();
                }
                break;
                
            case Loop.Radio_Loop:
                if(loop.clip == sfx_radio_loop && loop.isPlaying)
                    loop.Stop();
                else
                {
                    loop.clip = sfx_radio_loop;
                    loop.Play();
                }
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(new_Loop), new_Loop, null);
        }
    }

    public void PlaySfx(SFX new_sfx)
    {
        AudioClip random_sfx;
        
        switch (new_sfx)
        {
            case SFX.Clic:
                sfx.PlayOneShot(sfx_clic_possession);
                break;
            case SFX.Porte:
                random_sfx = porte_list[new Random().Next(0, 3)];
                sfx.PlayOneShot(random_sfx);
                break;
            case SFX.Fenetre:
                random_sfx = fenetre_list[new Random().Next(0, 3)];
                sfx.PlayOneShot(random_sfx);
                break;
            case SFX.Lampe:
                random_sfx = lampe_list[new Random().Next(0, 3)];
                sfx.PlayOneShot(random_sfx);
                break;
            case SFX.Coussin:
                random_sfx = coussin_list[new Random().Next(0, 3)];
                sfx.PlayOneShot(random_sfx);
                break;
            case SFX.Tableau:
                random_sfx = tableau_list[new Random().Next(0, 3)];
                sfx.PlayOneShot(random_sfx);
                break;
            case SFX.Radio:
                random_sfx = radio_list[new Random().Next(0, 3)];
                sfx.PlayOneShot(random_sfx);
                break;
            case SFX.Valise:
                random_sfx = valise_list[new Random().Next(0, 3)];
                sfx.PlayOneShot(random_sfx);
                break;
            case SFX.Plante:
                random_sfx = plante_list[new Random().Next(0, 3)];
                sfx.PlayOneShot(random_sfx);
                break;
            case SFX.Tel:
                random_sfx = tel_list[new Random().Next(0, 3)];
                sfx.PlayOneShot(random_sfx);
                break;
            case SFX.Placard:
                random_sfx = porte_placard_list[new Random().Next(0, 3)];
                sfx.PlayOneShot(random_sfx);
                break;
            case SFX.Tiroir:
                random_sfx = tiroir_list[new Random().Next(0, 3)];
                sfx.PlayOneShot(random_sfx);
                break;
            case SFX.Petit_Objet:
                random_sfx = petit_objet_list[new Random().Next(0, 3)];
                sfx.PlayOneShot(random_sfx);
                break;
            case SFX.TV_ON:
                sfx.PlayOneShot(sfx_tv_on);
                break;
            case SFX.Radio_ON:
                sfx.PlayOneShot(sfx_radio_on);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(new_sfx), new_sfx, null);
        }
        
    }
}