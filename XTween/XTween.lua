require 'XTween/Base/XTweenerGlobal'
require 'XTween/Base/XTweenCurv'
require 'XTween/Base/XTweener'
require 'XTween/Base/XTweenerGroup'
require 'XTween/Base/XTweenerGroupQueueItem'
require 'XTween/Base/XTweenObserver'
require 'XTween/Base/XTweenUpdater'

require 'XTween/XTweenerAction'
require 'XTween/XTweenerColor'
require 'XTween/XTweenerPosition'
require 'XTween/XTweenerRotaion'
require 'XTween/XTweenerScale'
require 'XTween/XTweenerText'
require 'XTween/XTweenerValue'
require 'XTween/XTweenerShake'

XTween = {}

local function GetXTweenerPosition(moveType, target, startValue, endValue, duration, playTimes, pingPong, curv, delay)
    if not playTimes then
        playTimes = 1
    end
    if not pingPong then
        pingPong = false
    end
    if not curv then
        curv = XTweenCurvType.Linear
    end
    local xTweener = XTweenerPosition.new()
    xTweener:Construction(target, startValue, endValue, duration, playTimes, moveType, pingPong, curv)
    if delay then
        xTweener:SetDelay(delay)
    end
    return xTweener
end

local function GetXTweenerValue( target, startValue, endValue, duration,valueChangeCallback, playTimes, pingPong, curv, delay)
    if not playTimes then
        playTimes = 1
    end
    if not pingPong then
        pingPong = false
    end
    if not curv then
        curv = XTweenCurvType.Linear
    end
    local xTweener = XTweenerValue.new()
    xTweener:Construction(target, startValue, endValue, duration,valueChangeCallback, playTimes, pingPong, curv)
    if delay then
        xTweener:SetDelay(delay)
    end
    return xTweener
end

local function GetXTweenerScale(target, startValue, endValue, duration, playTimes, pingPong, curv, delay)
    if not playTimes then
        playTimes = 1
    end
    if not pingPong then
        pingPong = false
    end
    if not curv then
        curv = XTweenCurvType.Linear
    end
    local xTweener = XTweenerScale.new()
    xTweener:Construction(target, startValue, endValue, duration, playTimes, pingPong, curv)
    if delay then
        xTweener:SetDelay(delay)
    end
    return xTweener
end

local function GetXTweenerRotation(rotationType, target, startEulerAngles, endEulerAngles, duration, playTimes, pingPong, curv, delay)
    if not playTimes then
        playTimes = 1
    end
    if not pingPong then
        pingPong = false
    end
    if not curv then
        curv = XTweenCurvType.Linear
    end
    local xTweener = XTweenerRotaion.new()
    xTweener:Construction(rotationType, target, startEulerAngles, endEulerAngles, duration, playTimes, pingPong, curv)
    if delay then
        xTweener:SetDelay(delay)
    end
    return xTweener
end

local function GetXTweenerColor(colorType, target, fromColor, toColor, duration, playTimes, pingPong, curv, delay)
    if not playTimes then
        playTimes = 1
    end
    if not pingPong then
        pingPong = false
    end
    if not curv then
        curv = XTweenCurvType.Linear
    end
    local xTweener = XTweenerColor.new()
    xTweener:Construction(colorType, target, fromColor, toColor, duration, playTimes, pingPong, curv);
    if delay then
        xTweener:SetDelay(delay)
    end
    return xTweener
end

local function GetXTweenerText(target, content, duration, playTimes, pingPong, curv, delay)
    local xTweener = XTweenerText.new()
    xTweener:Construction(target, content, duration, playTimes, pingPong, curv, delay)
    if delay then
        xTweener:SetDelay(delay)
    end
    return xTweener
end


















----接口部分

---@param target Transform
---@param duration float
---@param magnitude float
---@param delay int
function XTween.DoShake(target, duration, magnitude, delay)
    local tweener = XTweenerShake.new()
    tweener:Construction(target, duration, magnitude, delay)
    return tweener
end

---@param moveType XTweenMoveType
---@param target Transform
---@param startValue Vector3
---@param endValue Vector3
function XTween.DoMove(moveType, target, startValue, endValue, duration, curv, callBack, delay, playTimes, pingPong)
    if not playTimes then
        playTimes = 1
    end
    if not pingPong then
        pingPong = false
    end
    if not curv then
        curv = XTweenCurvType.Linear
    end
    local tweener = GetXTweenerPosition(moveType, target, startValue, endValue, duration, playTimes, pingPong, curv, delay)
    tweener:SetFinishCallback(callBack)
    -- tweener:SetTweenerData(userData)
    if delay then
        tweener:SetDelay(delay)
    end
    return tweener
end

---@param target Transform
---@param startValue float
---@param endValue float
---@param valueChangeCallback function(value) end
function XTween.DoValue(target,startValue, endValue, duration,valueChangeCallback,curv, delay,playTimes, pingPong)
    if not playTimes then
        playTimes = 1
    end
    if not pingPong then
        pingPong = false
    end
    if not curv then
        curv = XTweenCurvType.Linear
    end

    local xTweener = GetXTweenerValue(target, startValue, endValue, duration,valueChangeCallback, playTimes, pingPong, curv, delay)
    if delay then
        xTweener:SetDelay(delay)
    end
    return xTweener
end

---@param target Transform
---@param startValue Vector3
---@param endValue Vector3
function XTween.DoScale(target, startValue, endValue, duration, curv, callBack, delay, playTimes, pingPong)
    if not playTimes then
        playTimes = 1
    end
    if not pingPong then
        pingPong = false
    end
    if not curv then
        curv = XTweenCurvType.Linear
    end
    local xTweener = GetXTweenerScale(target, startValue, endValue, duration, playTimes, pingPong, curv, delay);
    if delay then
        xTweener:SetDelay(delay)
    end
    if callBack then
        xTweener:SetFinishCallback(callBack)
    end
    return xTweener
end

---@param rotationType XTweenRotationType
---@param target Transform
---@param startEulerAngles Vector3
---@param endEulerAngles Vector3
function XTween.DoRotate(rotationType, target, startEulerAngles, endEulerAngles, duration, curv, callBack, delay, playTimes, pingPong)
    if not playTimes then
        playTimes = 1
    end
    if not pingPong then
        pingPong = false
    end
    if not curv then
        curv = XTweenCurvType.Linear
    end
    local xTweener = GetXTweenerRotation(rotationType, target, startEulerAngles, endEulerAngles, duration, playTimes, pingPong, curv, delay);
    if delay then
        xTweener:SetDelay(delay)
    end
    if callBack then
        xTweener:SetFinishCallback(callBack)
    end
    return xTweener
end

function XTween.DoText(target, content, duration, curv, callBack, delay, playTimes, pingPong)
    if not playTimes then
        playTimes = 1
    end
    if not pingPong then
        pingPong = false
    end
    if not curv then
        curv = XTweenCurvType.Linear
    end
    local xTweener = GetXTweenerText(target, content, duration, playTimes, pingPong, curv, delay);
    if delay then
        xTweener:SetDelay(delay)
    end
    if callBack then
        xTweener:SetFinishCallback(callBack)
    end
    return xTweener
end

---@param colorType XTweenColorType
---@param target Transform
---@param startValue Color
---@param toColor Color
function XTween.DoColor(colorType, target, fromColor, toColor, duration, curv, delay, playTimes, pingPong)
    if not playTimes then
        playTimes = 1
    end
    if not pingPong then
        pingPong = false
    end
    if not curv then
        curv = XTweenCurvType.Linear
    end
    local xTweener = GetXTweenerColor(colorType, target, fromColor, toColor, duration, playTimes, pingPong, curv, delay);
    if delay then
        xTweener:SetDelay(delay)
    end

    return xTweener
end

function XTween.DoAction(delay, action)
    local tweenerAction = XTweenerAction.new():Construction()
    tweenerAction:SetDelay(delay)
    tweenerAction:SetAction(action)
    return tweenerAction
end

function XTween.CreateXTweenerGroup()
    return XTweenerGroup.new():Construction()
end

