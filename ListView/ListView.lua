ListViewScrollStyle ={
    Top = 1,
    Bottom = 2,
    Left = 3,
    Right = 4,
}


ListView = {
	name = 'ListView',
	gameObject = nil,
}

function ListView:New()
    log("function ListView:New()")

    local o = {}
	setmetatable(o,self)
    self.__index = self

	return o
end

function ListView:Awake()
    self.updateID = -1
    log("function ListView:Awake()")
end

function ListView:Start()
    self:SetUpdate()
    log("function ListView:Start()")
end

function ListView:SetUpdate()
    if self.updateID ~= -1 then
        return
    end
    self.updateID = UpdateBeat.Add(function(deltatime)
        if self.currentRows > 0 then
            local offset = self:GetScrollOffset()

            if math.abs(offset - self.tmpOffset) > 20 then
                -- logError("1111")
                self.tmpOffset = offset
                self:ShowCurrentRows(false)
            end
        end
    end)
end

function ListView:RemoveUpdate()
    if self.updateID == -1 then
        return
    end
    -- body
    UpdateBeat.Remove(self.updateID)
    self.updateID = -1
end

function ListView:OnEnable()
    self:SetUpdate()
    log("function ListView:OnEnable()")
end

function ListView:OnDisable()
    self:RemoveUpdate()
    log("function ListView:OnDisable()")
end

function ListView:OnDestroy()
    self:RemoveUpdate()
    log("function ListView:OnDestroy()")
end

--[[

    self.scrollview:cc.ScrollView, 
    public  contentView:cc.Node, 
    self.scrollBar:cc.Scrollbar, 
    self.scrollstyle:ListViewScrollStyle, 
    self.spacingForRowCallback:(row)=>number, 
    self.countOfRowsCallback:()=>number, 
    self.getCellCallback:()=>Cell, 
    self.cellForRow:(cell:Cell)=>void, 

    self.totalHeight, 
    self.unuseCell:Array<Cell> = [], 
    self.usingCell:[row]:Cell = , 
    self.posForRows:Array<number> = [], 
    self.spaceForRows:Array<number> = [], 
    self.offset = 0, 
    self.lastOffset = 0, 
    self.currentStartRow = -1, 
    self.currentEndRow = -1, 
    self.lastStartRow = -1, 
    self.lastEndRow = -1, 
    self.currentRows = 0, 

]]

function ListView:Init(
    scrollstyle,
    spacingForRowCallback,
    countOfRowsCallback,
    getCellCallback,
    cellForRow
)

    self.tmpOffset = 0
    -- self.offset = 0

    self.totalHeight = 0
    self.unuseCell = {}
    self.usingCell = {}
    self.posForRows = {}
    self.spaceForRows = {}
    -- self.offset = 0
    self.lastOffset = 0
    self.currentStartRow = -1
    self.currentEndRow = -1
    self.lastStartRow = -1
    self.lastEndRow = -1
    self.currentRows = 0

    self.vec2Cache = Vector2.New(0,0)
    self.sizeDelta = Vector2.New(0,0)
    
    self.spacingForRowCallback = spacingForRowCallback
    self.countOfRowsCallback = countOfRowsCallback
    self.getCellCallback = getCellCallback
    self.cellForRow = cellForRow
    self.scrollView = self.gameObject:AddComponent(typeof(ScrollRect))

    local viewportObj = GameObject("Viewport")
    viewportObj.transform.parent = self.gameObject.transform
    viewportObj:AddComponent(typeof(Image))
    local mask = viewportObj:AddComponent(typeof(Mask))
    mask.showMaskGraphic = false
    viewportObj.transform.localPosition = Vector3.New(0,0,0)
    viewportObj.transform.localScale = Vector3.New(1,1,1)

    local viewportObjRectTransform = viewportObj:GetComponent("RectTransform")
    viewportObjRectTransform.anchorMax = Vector2.New(1,1)
    viewportObjRectTransform.anchorMin = Vector2.New(0,0)
    viewportObjRectTransform.anchoredPosition = Vector2.New(0.5, 0.5)
    viewportObjRectTransform.sizeDelta = Vector2.New(0,0)

    local contentObj = GameObject("ContentView")
    local image = contentObj:AddComponent(typeof(Image))
    image.enabled = false
    self.contentViewRectTransform = contentObj:GetComponent("RectTransform")
    self.scrollView.content = self.contentViewRectTransform
    self.contentView = contentObj.transform

    self.listviewRectTransform = self.gameObject:GetComponent("RectTransform")
    self.sizeDelta.x = self.listviewRectTransform.sizeDelta.x
    self.sizeDelta.y = self.listviewRectTransform.sizeDelta.y
        
    self.contentViewRectTransform.sizeDelta = self.sizeDelta
    
    self.contentView.parent = viewportObj.transform
    self.contentView.localScale = Vector3.New(1,1,1)
    self.contentView.localPosition = Vector3.New(0,0,0)

    self:SetScrollStyle(scrollstyle)
end

function ListView:ScrollToTop()
    if self.scrollstyle == ListViewScrollStyle.Top then
        self.contentViewRectTransform.anchoredPosition = Vector2.zero
    elseif self.scrollstyle == ListViewScrollStyle.Bottom then
        
    elseif self.scrollstyle == ListViewScrollStyle.Left then
        
    elseif self.scrollstyle == ListViewScrollStyle.Right then
        
    end
end

function ListView:SetScrollStyle(scrollstyle)

    self.scrollstyle = scrollstyle
    if self.scrollstyle == ListViewScrollStyle.Top then
        self.vec2Cache.x = 0.5
        self.vec2Cache.y = 1
    elseif self.scrollstyle == ListViewScrollStyle.Bottom then
        self.vec2Cache.x = 0.5
        self.vec2Cache.y = 0
    elseif self.scrollstyle == ListViewScrollStyle.Left then
        self.vec2Cache.x = 0
        self.vec2Cache.y = 0.5
    elseif self.scrollstyle == ListViewScrollStyle.Right then
        self.vec2Cache.x = 1
        self.vec2Cache.y = 0.5
    end

    self.contentViewRectTransform.anchorMin = self.vec2Cache
    self.contentViewRectTransform.anchorMax = self.vec2Cache
    self.contentViewRectTransform.pivot = self.vec2Cache

    if self.scrollstyle == ListViewScrollStyle.Top or self.scrollstyle == ListViewScrollStyle.Bottom then
        self.scrollView.horizontal = false
        self.scrollView.vertical = true
    else
        self.scrollView.horizontal = true
        self.scrollView.vertical = false
    end
end

function ListView:GetScrollOffset()
    local scrollOffset = 0
    if self.scrollstyle == ListViewScrollStyle.Top then
        scrollOffset = self.contentViewRectTransform.anchoredPosition.y
    elseif self.scrollstyle == ListViewScrollStyle.Bottom then
        scrollOffset = -self.contentViewRectTransform.anchoredPosition.y
    elseif self.scrollstyle == ListViewScrollStyle.Left then
        scrollOffset = -self.contentViewRectTransform.anchoredPosition.x
    elseif self.scrollstyle == ListViewScrollStyle.Right then
        scrollOffset = self.contentViewRectTransform.anchoredPosition.x
    end

    if scrollOffset < 0 then
        scrollOffset = 0
    end

    return scrollOffset
end

function ListView:Refresh()
    self.posForRows = {}
    self.spaceForRows = {}
    self.totalHeight = 0
    
    self:HideAll()

    -- self.offset = 0
    self.lastOffset = 0
    self.currentStartRow = -1
    self.currentEndRow = -1
    self.lastStartRow = -1
    self.lastEndRow = -1
    self.currentRows = 0

    local countOfRows = math.floor(self.countOfRowsCallback())
    self.currentRows = countOfRows

    for i = 1,countOfRows do
        local space = math.floor(self.spacingForRowCallback(i))
        table.insert(self.posForRows,self.totalHeight)
        table.insert(self.spaceForRows,space)
        self.totalHeight = self.totalHeight + space
    end

    if self.scrollstyle == ListViewScrollStyle.Top then
        self.vec2Cache.x = self.contentViewRectTransform.sizeDelta.x
        self.vec2Cache.y = self.totalHeight
        
        self.contentViewRectTransform.sizeDelta = self.vec2Cache

        -- logError("self:GetScrollOffset() " .. self:GetScrollOffset()
        --  .. " self.totalHeight " .. self.totalHeight 
        --  .. " self.sizeDelta.y " .. self.sizeDelta.y
        --  .. " self.totalHeight - self.sizeDelta.y " .. self.totalHeight - self.sizeDelta.y)

        local maxOffset = self.totalHeight - self.sizeDelta.y
        
        if maxOffset > 0 then
            if self:GetScrollOffset() > maxOffset then
                self.contentViewRectTransform.anchoredPosition = Vector2.New(0,self.totalHeight - self.sizeDelta.y)
            end
        else
            self.contentViewRectTransform.anchoredPosition = Vector2.New(0,0)
        end
        
    end

    self:ShowCurrentRows(true)
end

function ListView:GetRowsShouldShow(forceRefresh)
    local offset = self:GetScrollOffset()

    if offset == self.lastOffset and forceRefresh == false then
        return nil 
    end

    -- logError("self.currentStartRow " .. self.currentStartRow .. " #self.posForRows " .. #self.posForRows
    -- .. " offset " .. offset .. " self.totalHeight - self.sizeDelta.y " .. self.totalHeight - self.sizeDelta.y)

    if offset <= 0 then
        self.currentStartRow = 1
    elseif offset >= self.totalHeight - self.sizeDelta.y then
        self.currentEndRow = self.currentRows
        local i = self.currentEndRow
        while(i > 0) do
            self.currentStartRow = i
            -- logError("set self.currentStartRow 333  " .. self.currentStartRow)
            if self.posForRows[i] <= offset then
                break
            end
            i = i - 1
        end
    else
        -- logError("offset" .. offset .. " self.lastOffset " .. self.lastOffset .. " self.currentStartRow " .. self.currentStartRow)
        if offset > self.lastOffset then
            local i = self.currentStartRow
            --强制刷新需要从1开始算
            if forceRefresh == true then
                i = 1
            end
            while(i<=#self.posForRows) do
                
                self.currentStartRow = i
                -- logError("set self.currentStartRow 111  " .. self.currentStartRow)
                if self.posForRows[i] == offset 
                    or (self.posForRows[i] < offset and self.posForRows[i] + self.spaceForRows[i] > offset) then
                    break
                end
                i = i + 1
            end
        else
            local i = self.currentEndRow
            while(i>0) do
                self.currentStartRow = i
                -- logError("set self.currentStartRow 222  " .. self.currentStartRow 
                -- .. " self.posForRows[i] " .. self.posForRows[i] .. " offset " .. offset
                -- .. " self.posForRows[i] + self.spaceForRows[i] " .. self.posForRows[i] + self.spaceForRows[i])
                if self.posForRows[i] == offset 
                    or (self.posForRows[i]< offset and self.posForRows[i] + self.spaceForRows[i] > offset) then
                        break
                end
                i = i - 1
            end
        end
    end

    local i = self.currentStartRow
    while(i<=#self.posForRows) do
        self.currentEndRow = i
        -- logError("i " .. i .. " #self.spaceForRows " .. #self.spaceForRows .. " #self.posForRows " .. #self.posForRows)
        if self.posForRows[i] + self.spaceForRows[i] > offset + self.sizeDelta.y then
            break
        end
        i = i + 1
    end

    -- logError("self.currentStartRow " .. self.currentStartRow .. " self.currentEndRow " .. self.currentEndRow)

    ---多显示上下一个
    if self.currentStartRow > 1 then
        self.currentStartRow = self.currentStartRow - 1
    end

    if self.currentEndRow < self.currentRows then
        self.currentEndRow = self.currentEndRow + 1
    end

    self.lastOffset = offset
end

function ListView:ShowCurrentRows(forceRefresh)
    if self.currentRows == 0 then
        self:HideAll()
        return nil
    end

    self.lastStartRow = self.currentStartRow
    self.lastEndRow = self.currentEndRow
    self:GetRowsShouldShow(forceRefresh)

    --[[

    //6种区间关系
        /*
        1.---       
            |
         ---|-
         |  |
         |  |
         |  |
         ---|-
          ---  

        2.
         ---
         |
        -|---
         |  |
         |  |
        -|---
         |  
         ---

         3.
         ----
         |
         |
         |
         |-----
         |    |
         ---- |
              |
              |
         ------

         4.
       -----
           |
       ----|
       |   |
       |----   
       |
       |
       -----

       5.
       ---
       |
       |
       ---

       ---
         |
         |
       ---
       
       6.
       ---
         |
         |
       ---

       ---
       |
       |
       ---

       *****************
       ---
       |
       |
       --- 是last

       ---
         |
         |
       --- 是current

    ]]

    local i = 0
    if self.lastStartRow ~= self.currentStartRow or self.lastEndRow ~= self.currentEndRow or forceRefresh == true then
        local shouldCallCellForRow = not(forceRefresh)
        if self.lastStartRow >= self.currentStartRow and self.lastEndRow <= self.currentEndRow then
            --logError("1")
            i = self.currentStartRow
            while(i<self.lastStartRow) do
                self:ShowRow(i,shouldCallCellForRow)
                i = i + 1
            end

            i = self.lastEndRow + 1
            while(i<=self.currentEndRow) do
                self:ShowRow(i,shouldCallCellForRow)
                i = i + 1
            end
        
        elseif self.lastStartRow <= self.currentStartRow and self.lastEndRow >= self.currentEndRow then
            --logError("2")
            i = self.lastStartRow
            while(i<self.currentStartRow) do
                self:HideRow(i)
                i = i + 1
            end
            
            i = self.currentEndRow + 1
            while(i<=self.lastEndRow) do
                self:HideRow(i)
                i = i + 1
            end
        
        elseif self.lastStartRow <= self.currentStartRow and self.lastEndRow >= self.currentStartRow and self.lastEndRow <= self.currentEndRow then
            --logError("3")
            i = self.lastStartRow
            while(i<self.currentStartRow) do
                self:HideRow(i)
                i = i + 1
            end

            i = self.lastEndRow + 1
            while(i <= self.currentEndRow) do
                self:ShowRow(i,shouldCallCellForRow)
                i = i + 1
            end
            
        
        elseif self.lastStartRow <= self.currentEndRow and self.lastEndRow >= self.currentEndRow and self.lastStartRow >= self.currentStartRow then
            --logError("4")
            i = self.currentEndRow + 1
            while(i <= self.lastEndRow) do
                self:HideRow(i)
                i = i + 1
            end

            i = self.currentStartRow
            while(i<self.lastStartRow) do
                self:ShowRow(i,shouldCallCellForRow)
                i = i + 1
            end
        
        elseif self.lastEndRow <= self.currentStartRow or self.lastStartRow >= self.currentEndRow then
            --logError("5")
            i = self.lastStartRow
            while(i<=self.lastEndRow) do
                if i ~= self.currentStartRow or i ~= self.currentEndRow then
                    self:HideRow(i)
                end
                i = i + 1
            end

            i = self.currentStartRow
            while(i<= self.currentEndRow) do
                self:ShowRow(i,shouldCallCellForRow)
                i = i + 1
            end
        end
            
    
        if forceRefresh == true then
            i = self.currentStartRow
            while(i<=self.currentEndRow) do
                local cell = self.usingCell[i]
                self.cellForRow(cell)
                i = i + 1
            end
        end
    end
    
end

function ListView:ShowRow(row,shouldCallCellForRow)
    local cell = nil

    -- logError("***********33333")
    -- logError(self)

    if #self.unuseCell > 0 then
        local findIndex = -1
        --先找到之前相同的row
        for i = 1,#self.unuseCell do
            if self.unuseCell[i].row == row then
                findIndex = i
                break;
            end
        end

        if findIndex == -1 then
            findIndex = 1
        end
        cell = self.unuseCell[findIndex]
        table.remove(self.unuseCell,findIndex)
    else
        -- logError("getCellCallback")
        cell = self.getCellCallback()
        cell.rectTransform = cell.content:GetComponent("RectTransform")
        cell.content.transform.parent = self.contentView
        cell.content.transform.localScale = Vector3.New(1,1,1)
        cell.content.transform.localPosition = Vector3.New(0,0,0)
        
        if self.scrollstyle == ListViewScrollStyle.Top then
            self.vec2Cache.x = 0.5
            self.vec2Cache.y = 1
        elseif self.scrollstyle == ListViewScrollStyle.Bottom then
            self.vec2Cache.x = 0.5
            self.vec2Cache.y = 0
        elseif self.scrollstyle == ListViewScrollStyle.Left then
            self.vec2Cache.x = 0
            self.vec2Cache.y = 0.5
        elseif self.scrollstyle == ListViewScrollStyle.Right then
            self.vec2Cache.x = 1
            self.vec2Cache.y = 0.5
        end

        cell.rectTransform.anchorMin = self.vec2Cache
        cell.rectTransform.anchorMax = self.vec2Cache
        cell.rectTransform.pivot = self.vec2Cache

    end
    cell.row = row
    cell.content:SetActive(true)

    if self.scrollstyle == ListViewScrollStyle.Top then
        local pos = cell.rectTransform.anchoredPosition
        pos.x = 0
        pos.y = -self.posForRows[row]
        cell.rectTransform.anchoredPosition = pos

        self.vec2Cache.x = self.sizeDelta.x
        self.vec2Cache.y = self.spaceForRows[row]
        cell.rectTransform.sizeDelta = self.vec2Cache
    end

    if shouldCallCellForRow == true then
        self.cellForRow(cell)
    end

    self.usingCell[row] = cell
end

function ListView:HideRow(row)
    local cell = self.usingCell[row]
    if cell ~= nil then
        cell.content:SetActive(false)
        table.insert(self.unuseCell,cell)
        self.usingCell[row] = nil
    end
end

function ListView:HideAll()
    -- local i = self.currentStartRow
    -- while(i<=self.currentEndRow) do
    --     if i ~= self.currentStartRow or i ~= self.currentEndRow then
    --         self:HideRow(i)
    --     end
    --     i = i + 1
    -- end

    for row,cell in pairs(self.usingCell) do
        cell.content:SetActive(false)
        table.insert(self.unuseCell,cell)
    end
    self.usingCell = {}
end


