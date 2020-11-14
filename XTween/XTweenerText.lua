---@class XTweenerText:XTweener
XTweenerText = class(XTweener)

---@param target UnityEngine.UI.Text
---@param content string
---@param duration float
---@param playTimes int
---@param pingPong bool
---@param curv XTweenCurvType
function XTweenerText:Construction(target, content, duration, playTimes, pingPong, curv)
    self:ConstructionCallBySubClass()
    if not XTweenCurv.CheckCurvExit(curv) then
        logError('添加的曲线类型不存在！！！！！curv='..tostring(curv))
        logError(debug.traceback())
    end
    self:SetTarget(target.transform)
    --self.target = target.transform
    self.content = content
    self.textComponent = target

    --以下继承父类属性
    self.duration = duration
    self.curvType = curv
    self.pingPong = pingPong
    self.repeatTimes = playTimes
end

function XTweenerText:Update(deltaTime)
    self:UpdateCallBySubClass(deltaTime)

    if self.beReplaced then
        self:XTweenerCompleteFinish()
        return
    end

    local length = math.floor(self.animationProgress * string.len(self.content))
    self.textComponent.text = string.sub(self.content, 1, length)--.Substring(0, length)

    self:XTweenerCompleteFinish()


end


function XTweenerText:OnPlay()
    if not self.startValue then
        if self.textComponent.text then
            self.startValue = #(self.textComponent.text)
        end
    end
end

function XTweenerText:GetTypeMark()
    return 'XTweenerText'
end