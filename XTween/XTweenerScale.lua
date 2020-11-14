---@class XTweenerScale:XTweener
XTweenerScale = class(XTweener)

---@param target UnityEngine.Transform
---@param startValue Vector3
---@param endValue Vector3
---@param duration float
---@param playTimes int
---@param pingPong bool
---@param curv XTweenCurvType
function XTweenerScale:Construction(target, startValue, endValue, duration, playTimes, pingPong, curv)
    self:ConstructionCallBySubClass()
    if not XTweenCurv.CheckCurvExit(curv) then
        logError('添加的曲线类型不存在！！！！！curv='..tostring(curv))
        logError(debug.traceback())
    end
    self:SetTarget(target)
    --self.target = target
    self.startValue = startValue
    self.endValue = endValue

    --以下继承父类属性
    self.duration = duration
    self.curvType = curv
    self.pingPong = pingPong
    self.repeatTimes = playTimes
end

function XTweenerScale:Update(deltaTime)
    self:UpdateCallBySubClass(deltaTime)

    if self.beReplaced then
        self:XTweenerCompleteFinish()
        return
    end
    ---@type Vector3
    local value = Vector3.Lerp(self.startValue, self.endValue, self.animationProgress)
    self.target.localScale = value

    self:XTweenerCompleteFinish()

end


function XTweenerScale:OnPlay()
    if not self.startValue then
        self.startValue = self.target.localScale
    end
end


function XTweenerScale:GetTypeMark()
    return 'XTweenerScale'
end