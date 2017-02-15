using prjPetAdoption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjPetAdoption.ViewModels
{
    public class AllAniDataViewModel
    {

        public  C__MigrationHistory  C__MigrationHistory { get; set; }
        public  animalData  animalData { get; set; }
        public  animalData_Condition  animalData_Condition { get; set; }
        public  animalData_Pic  animalData_Pic { get; set; }
        public  animalData_Type  animalData_Type { get; set; }
        public  AspNetRoles  AspNetRoles { get; set; }
        public  AspNetUserClaims  AspNetUserClaims { get; set; }
        public  AspNetUserLogins  AspNetUserLogins { get; set; }
        public  AspNetUsers  AspNetUsers { get; set; }
        public  board  board { get; set; }
        public  follow  follow { get; set; }
        public  forum  forum { get; set; }
        public  forum_reply  forum_reply { get; set; }
        public  map  map { get; set; }
        public  Msg  Msg { get; set; }
        public  petFAQ  petFAQ { get; set; }
        public  petKnows  petKnows { get; set; }
        public  petTodo  petTodo { get; set; }
        public  remind  remind { get; set; }
        public  search  search { get; set; }
        public  sysdiagrams  sysdiagrams { get; set; }
        public  todoData  todoData { get; set; }
        public  aniDataAll  aniDataAll { get; set; }
        public aniDataPicOne aniDataPicOne { get; set; }
        public followAni followAni { get; set; }
        public MsgUser MsgUser { get; set; }

        public IEnumerable<animalData> animalDataList { get; set; }
        public IEnumerable<animalData_Pic> animalData_PicList { get; set; }
        public IEnumerable<aniDataAll> aniDataAllList { get; set; }
        public IEnumerable<aniDataPic> aniDataPicList { get; set; }
        public IEnumerable<aniDataPicOne> aniDataPicOneList { get; set; }
        public IEnumerable<map> mapList { get; set; }
        public IEnumerable<boardUser> boardUserList { get; set; }
        public IEnumerable<animalData_Condition> animalData_ConList { get; set; }
        public IEnumerable<followAni> followAniList { get; set; }
        public IEnumerable<MsgUser> MsgUserList { get; set; }
    }
}