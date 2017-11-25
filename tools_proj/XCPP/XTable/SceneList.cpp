/*
 * <auto-generated>
 *	 The code is generated by tools
 *	 Edit manually this code will lead to incorrect behavior
 * </auto-generated>
 */

#include "SceneList.h"

SceneList::SceneList(void)
{
	name = UNITY_STREAM_PATH + "Table/SceneList.bytes";
	ReadTable();
}

void SceneList::ReadTable()
{
	LOG("read:"+name);
	Open(name.c_str());
	long long filesize =0;
	int lineCnt = 0;
	Read(&filesize);
	Read(&lineCnt);
	m_data.clear();
	for(int i=0;i<lineCnt;i++)
	{
		SceneListRow *row = new SceneListRow();
		
		Read(&(row->sceneid));
		ReadString(row->comment);
		Read(&(row->scenetype));
		Read(&(row->isstaticscene));
		Read(&(row->pretask));
		ReadArray<int>(row->prescene);
		Read(&(row->requiredlevel));
		Read(&(row->syncmode));
		Read(&(row->switchtoself));
		Read(&(row->canreconnect));
		Read(&(row->endtimeout));
		Read(&(row->iscanquit));
		Read(&(row->delaytransfer));
		Read(&(row->linerolecount));
		ReadString(row->buff);
		ReadString(row->scenefile);
		ReadString(row->unityscenefile);
		ReadString(row->scenepath);
		ReadString(row->bgm);
		ReadString(row->blockfilepath);
		ReadArray<int>(row->operationsettings);
		ReadString(row->startpos);
		ReadArray<int>(row->startface);
		ReadArray<float>(row->startrot);
		ReadString(row->chapter);
		ReadArray<int>(row->uipos);
		ReadString(row->boxuipos);
		ReadString(row->uiicon);
		ReadString(row->fatiguecost);
		ReadArray<int>(row->viewabledroplist);
		Read(&(row->exp));
		ReadArray<int>(row->drop);
		ReadArray<int>(row->drop1);
		ReadArray<int>(row->drop2);
		ReadArray<int>(row->drop3);
		ReadArray<int>(row->drop4);
		ReadArray<int>(row->drop5);
		Read(&(row->money));
		Read(&(row->firstdownexp));
		ReadArray<int>(row->firstdowndrop);
		Read(&(row->firstdownmoney));
		ReadString(row->firstsss);
		ReadString(row->expsealreward);
		ReadString(row->scenechest);
		ReadString(row->diamonddropid);
		ReadString(row->golddropid);
		ReadString(row->silverdropid);
		ReadString(row->copperdropid);
		ReadString(row->sbox);
		ReadString(row->ssbox);
		ReadString(row->sssbox);
		Read(&(row->isboss));
		Read(&(row->recommendpower));
		ReadString(row->bossavatar);
		ReadString(row->bossicon);
		ReadString(row->endcutscene);
		ReadString(row->endcutscenetime);
		ReadString(row->wincondition);
		ReadString(row->losecondition);
		ReadString(row->windelaytime);
		Read(&(row->daylimit));
		Read(&(row->cooldown));
		ReadString(row->daylimitgroupid);
		Read(&(row->candrawbox));
		Read(&(row->hasflyout));
		ReadString(row->dynamicscene);
		ReadString(row->envset);
		Read(&(row->canrevive));
		ReadString(row->revivenumb);
		ReadString(row->revivecost);
		ReadString(row->revivemoneycost);
		ReadString(row->revivetime);
		ReadString(row->revivebuff);
		ReadString(row->revivebufftip);
		Read(&(row->canpause));
		Read(&(row->showup));
		ReadString(row->loadingtips);
		ReadString(row->loadingpic);
		Read(&(row->scenecannavi));
		Read(&(row->showautofight));
		Read(&(row->showbattlestatistics));
		ReadString(row->randomtasktype);
		ReadString(row->randomtaskspecify);
		Read(&(row->usesupplement));
		Read(&(row->hurtcoef));
		ReadString(row->minimap);
		ReadArray<int>(row->minimapsize);
		ReadString(row->minimapoutsize);
		Read(&(row->minimaprotation));
		ReadString(row->staticminimapcenter);
		ReadString(row->sceneai);
		ReadString(row->pptcoff);
		ReadString(row->guildexpbounus);
		ReadString(row->failtext);
		ReadString(row->leavescenetip);
		ReadString(row->recommendhint);
		ReadString(row->teaminfodefaulttab);
		Read(&(row->combattype));
		Read(&(row->sweepneedppt));
		ReadArray<int>(row->timecounter);
		Read(&(row->hascombobuff));
		Read(&(row->displaypet));
		ReadString(row->autoreturn);
		ReadString(row->storydriver);
		Read(&(row->minpasstime));
		Read(&(row->showskill));
		Read(&(row->shownormalattack));
		ReadString(row->winconditiontips);
		ReadString(row->battleexplaintips);
		ReadString(row->dps);
		ReadString(row->canviprevive);
		Read(&(row->hideteamindicate));
		ReadString(row->shieldsight);
		Read(&(row->specifiedtargetlocatedrange));
		ReadString(row->spactivityactivedrop);
		ReadString(row->spactivitydrop);
		ReadString(row->viprevivelimit);
		m_data.push_back(*row);
	}
	this->Close();
}

void SceneList::GetRow(int idx,SceneListRow* row)
{
	size_t len = m_data.size();
	if(idx<len)
	{
		*row = m_data[idx];
	}
	else
	{
		LOG("eror, SceneList index out of range, size: "+tostring(len)+" idx: "+tostring(idx));
	}
}

int SceneList::GetLength()
{
	return (int)m_data.size();
}


extern "C"
{
	SceneList *scenelist;

	int iGetSceneListLength()
	{
		if(scenelist==NULL)
		{
			scenelist = new SceneList();
		}
		return scenelist->GetLength();
	}

	void iGetSceneListRow(int suitid,SceneListRow* row)
	{
		if(scenelist==NULL)
		{
			scenelist = new SceneList();
		}
		scenelist->GetRow(suitid,row);
	}
}