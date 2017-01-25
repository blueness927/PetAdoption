using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjPetAdoption.Models
{
    public class OpenData
    {

        [Display(Name = "動物編號")]
        public string animal_id { get; set; }

        [Display(Name = "區域編號")]
        public string animal_subid { get; set; }

        [Display(Name = "縣市編號")]
        public string animal_area_pkid { get; set; }

        [Display(Name = "收容所編號")]
        public string animal_shelter_pkid { get; set; }

        [Display(Name = "動物實際所在地")]
        public string animal_place { get; set; }

        [Display(Name = "動物類型")]
        public string animal_kind { get; set; }

        [Display(Name = "動物性別")]
        public string animal_sex { get; set; }

        [Display(Name = "動物體型")]
        public string animal_bodytype { get; set; }

        [Display(Name = "動物毛色")]
        public string animal_colour { get; set; }

        [Display(Name = "動物年紀")]
        public string animal_age { get; set; }

        [Display(Name = "是否絕育")]
        public string animal_sterilization { get; set; }

        [Display(Name = "是否施打狂犬疫苗")]
        public string animal_bacterin { get; set; }

        [Display(Name = "動物尋獲地")]
        public string animal_foundplace { get; set; }

        [Display(Name = "動物網頁標題")]
        public string animal_title { get; set; }

        [Display(Name = "動物狀態")]
        public string animal_status { get; set; }

        [Display(Name = "資料備註")]
        public string animal_remark { get; set; }

        [Display(Name = "其他")]
        public string animal_caption { get; set; }

        [Display(Name = "開放認養時間(起)")]
        public string animal_opendate { get; set; }

        [Display(Name = "開放認養時間(迄)")]
        public string animal_closeddate { get; set; }

        [Display(Name = "動物資料異動時間")]
        public string animal_update { get; set; }

        [Display(Name = "動物資料建立時間")]
        public string animal_createtime { get; set; }

        [Display(Name = "動物所屬收容所名稱")]
        public string shelter_name { get; set; }

        [Display(Name = "圖片名稱(原始名稱)")]
        public string album_name { get; set; }

        [Display(Name = "圖片名稱")]
        public string album_file { get; set; }

        [Display(Name = "圖片 base64 編碼")]
        public string album_base64 { get; set; }

        [Display(Name = "異動時間")]
        public string album_update { get; set; }

        [Display(Name = "資料更新時間")]
        public string cDate { get; set; }

        [Display(Name = "地址")]
        public string shelter_address { get; set; }

        [Display(Name = "連絡電話")]
        public string shelter_tel { get; set; }
      
    }
}