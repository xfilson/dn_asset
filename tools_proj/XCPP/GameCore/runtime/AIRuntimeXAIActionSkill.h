/*
* <auto-generated>
*	 The code is generated by tools
*	 Edit manually this code will lead to incorrect behavior
* </auto-generated>
*/

#ifndef  __AIRuntimeXAIActionSkill__
#define  __AIRuntimeXAIActionSkill__

#include "../ai/AIBehaviour.h"
#include "../GameObject.h"
#include "../Vector3.h"
#include "../ai/AITreeImpleted.h"

class XEntity;

class AIRuntimeXAIActionSkill :public AIBase
{
public:
	~AIRuntimeXAIActionSkill();
	virtual void Init(AITaskData* data);
	virtual AIStatus OnTick(XEntity* entity);
	

private:
	std::string StringmAIArgSkillScript;
	GameObject* GameObjectmAIArgTarget;
	
};

#endif