local TimerEvent = {

}

function TimerEvent:New()
    local o = {}
	setmetatable(o,self)
    self.__index = self
    
    o.id = 0
    o.delay = 0
    o.interval = 0
    o.action = nil
    o.parm = nil
    o.repeatTimes = 0
    o.timeCal = 0
    o.currentTime = 0
    o.keepAlive = false
    o.haveRunDelayAction = false
    
	return o
end
---@class TimerManager
TimerManager = {
}
---@return TimerManager
function TimerManager:GetInstance()
    if self.instance == nil then
        self.instance = TimerManager:New()
    end
    return self.instance
end

local function DeleteTimerEvents(self)
    if #self.listIDWaitToRemove == 0 then
        return
    end
    for i = 1,#self.listIDWaitToRemove,1 do
        self.dicTimers[self.listIDWaitToRemove[i]] = nil
    end
    self.listIDWaitToRemove = {}
end

local function UpdateFunc(self,dt)
    DeleteTimerEvents(self)

    for id,timerEvent in pairs(self.dicTimers) do
        timerEvent.timeCal = timerEvent.timeCal + dt

        if timerEvent.timeCal > timerEvent.delay and timerEvent.haveRunDelayAction == false then
            timerEvent.haveRunDelayAction = true
            timerEvent.action(timerEvent.parm)
        end
        if timerEvent.repeatTimes ~= 1 then
            if timerEvent.timeCal > timerEvent.delay + (timerEvent.currentTime + 1) * timerEvent.interval then
                timerEvent.action(timerEvent.parm)
                timerEvent.currentTime = timerEvent.currentTime + 1
            end
        end
        if timerEvent.repeatTimes ~= -1 then
            if timerEvent.haveRunDelayAction == true and timerEvent.currentTime >= timerEvent.repeatTimes - 1 then
                table.insert(self.listIDWaitToRemove,timerEvent.id)
            end
        end
    end

    DeleteTimerEvents(self)
end

function TimerManager:New()
    local o = {}
	setmetatable(o,self)
    self.__index = self
    
    o.id = 0
    o.dicTimers = {}
    o.listIDWaitToRemove = {}

    UpdateBeat.Add(function(deltatime)
        UpdateFunc(o,deltatime)
    end)

	return o
end

---
---[action] 延时执行的方法
---[delay] 单位是秒
---[parm] action传入参数,默认null
---[repeatTimes] 总过执行次数，0和1作用一样，大于1才会执行多次
---[interval] 延时执行后重复的间隔,0为interval=delay，默认0
---[keepAlive] true=保持计时器不被清除，必须调用指定的方法才能清除这个计时器，默认false
---
function TimerManager:CallActionDelay(action,delay,parm,repeatTimes,interval,keepAlive)
    if delay == 0 then
        delay = 0.01
    end

    if repeatTimes == nil then
        repeatTimes = 1
    end
    if repeatTimes <= -1 then
        repeatTimes = -1
    end

    if repeatTimes == 0 then
        repeatTimes = 1
    end

    if interval == nil or interval <= 0 then
        interval = delay
    end

    if keepAlive == nil then
        keepAlive = false
    end

    local timerEvent = TimerEvent:New()
    timerEvent.id = self.id
    timerEvent.action = action
    timerEvent.delay = delay
    timerEvent.parm = parm
    timerEvent.repeatTimes = repeatTimes
    timerEvent.interval = interval
    timerEvent.keepAlive = keepAlive
    self.dicTimers[timerEvent.id] = timerEvent
    
    self.id = self.id + 1

    return timerEvent.id
end

function TimerManager:DeleteTimer(id)
    if id == nil then 
        return
    end
    if self.dicTimers[id] ~= nil then
        table.insert(self.listIDWaitToRemove,id)
    end
end

function TimerManager:ClearAll()
    for id,timerEvent in pairs(self.dicTimers) do
        self:DeleteTimer(id)
    end
end

function TimerManager:ClearAllButKeepLive()
    for id,timerEvent in pairs(self.dicTimers) do
        if not timerEvent.keepAlive then
            self:DeleteTimer(id)
        end
    end
end

-- TimerManager = {}
-- function TimerManager.CallActionDelay(action,delay,parm,repeatTimes,interval,keepAlive)
--     TimerManager:GetInstance():CallActionDelay(action,delay,parm,repeatTimes,interval,keepAlive)
-- end




