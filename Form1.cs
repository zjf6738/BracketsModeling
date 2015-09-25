using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Kitware.VTK;


// 整个程序由vtk负责渲染各种三维目标
// C#使用了很多之前我没有使用的技术，例如命名空间。
// 程序里头还使用了很多中文，我也是晕了

// 前一个分支所做的工作都出现问题，目前从该问题点重新编写代码WFA1


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private string FilePath;        //文件路径
        private string FileFullName;   //文件
        private vtkActor actor;
        private vtkPolyDataMapper mapper;
        private vtkPolyData originalMesh;
        vtkRenderWindow renderWindow;
        vtkRenderer renderer;
        bool MousePick = false;

        int pickedId;   //鼠标点击所获取的id
        double[] posit; //鼠标点击点的三维坐标

        vtkCellPicker picker;
        vtkObject.vtkObjectEventHandler InteractorHandler = null;
        vtkInteractorStyleUser UserStyle = null;
        vtkObject.vtkObjectEventHandler UserHandler = null;
        vtkOutputWindow ErrorWindow = null;
        vtkObject.vtkObjectEventHandler ErrorHandler = null;
        
        public Form1()
        {
            InitializeComponent();
            this.Width = 900;
            this.Height = 600;
        }

        private void renderWindowControl1_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 读取stl文件，并在窗口进行显示，并设置全局变量originalMesh
        /// </summary>
        private void ReadSTL() { 
            //Path to vtk data must be set as an environment variable
            //VTK_DATA_ROOT=""
            vtkSTLReader reader = vtkSTLReader.New();
            reader.SetFileName(FileFullName);
            reader.Update();
            mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(reader.GetOutputPort());

            actor = vtkActor.New();
            actor.SetMapper(mapper);
            //get a reference to the renderwindow of our renderWindowControll
            renderWindow = renderWindowControl1.RenderWindow;
            //renderer
            renderer = renderWindow.GetRenderers().GetFirstRenderer();
            //移除之前所有prop
            renderer.RemoveAllViewProps();
            //set background color
            renderer.SetBackground(0.2, 0.3, 0.4);
            //add our actor to the renderer
            renderer.AddActor(actor);
            originalMesh = vtkPolyData.New();
            originalMesh.DeepCopy(reader.GetOutput());
            tb_numOfPoint.Text = originalMesh.GetNumberOfPoints().ToString();
            
            //creat a cell picker
            picker = vtkCellPicker.New();
            vtkRenderWindowInteractor iren = renderWindow.GetInteractor();
            iren.SetPicker(picker);

            renderer.ResetCamera();
            renderWindow.Render();
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            网格ToolStripMenuItem.Checked = false;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                FileFullName = openFileDialog1.FileName;
                FilePath = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);
                tb_filename.Text = openFileDialog1.SafeFileName;
                try
                {
                    ReadSTL();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
                }

            }
        }

        private void 网格ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            网格ToolStripMenuItem.Checked=!网格ToolStripMenuItem.Checked;
            int n = actor.GetProperty().GetRepresentation();         //  2:Surface   1:Wireframe
            if (网格ToolStripMenuItem.Checked && n == 2)
                actor.GetProperty().SetRepresentationToWireframe();
            else if (!网格ToolStripMenuItem.Checked && n == 1)
                actor.GetProperty().SetRepresentationToSurface();
            renderWindow.Render();
        }

        /// <summary>
        /// 增加mesh的网格密度，根据numberOfAlgorithm选择所使用算法，numberOfSubdivision为细分的参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_subdivision_Click(object sender, EventArgs e)
        {
            int numberOfSubdivision = 2;
            int numberOfAlgorithm = 0;
            //建立一个超类指针(vtkPolyDataAlgorithm)来更加容易的使用不同类型的细分滤波器
            //但是通常情况下，建立一个所使用所滤波器类型的指针
            //例如：<vtkLinearSubdivisionFilter> subdivisionFilter=<vtkLInearSubdivisionFilter>.New();
            vtkPolyDataAlgorithm subdivisionFilter;
            switch (numberOfAlgorithm) { 
                case 0:
                    subdivisionFilter = vtkLinearSubdivisionFilter.New();
                    ((vtkLinearSubdivisionFilter)subdivisionFilter).SetNumberOfSubdivisions(numberOfSubdivision);
                    break;
                case 1:
                    subdivisionFilter = vtkLoopSubdivisionFilter.New();
                    ((vtkLoopSubdivisionFilter)subdivisionFilter).SetNumberOfSubdivisions(numberOfSubdivision);
                    break;
                case 2:
                    subdivisionFilter = vtkButterflySubdivisionFilter.New();
                    ((vtkButterflySubdivisionFilter)subdivisionFilter).SetNumberOfSubdivisions(numberOfSubdivision);
                    break;
                default:
                    subdivisionFilter = vtkButterflySubdivisionFilter.New();
                    ((vtkButterflySubdivisionFilter)subdivisionFilter).SetNumberOfSubdivisions(numberOfSubdivision);
                    break;
            }
            //subdivisionFilter.SetInput(originalMesh);
            subdivisionFilter.SetInputConnection(originalMesh.GetProducerPort());
            subdivisionFilter.Update();
            //更新orginalMesh数据
            originalMesh = subdivisionFilter.GetOutput();
            mapper.SetInput(originalMesh);
            //mapper.SetInputConnection(subdivisionFilter.GetOutputPort());
            renderWindow.Render();
            
            //更新显示点数
            vtkPolyData temp = vtkPolyData.New();
            temp = subdivisionFilter.GetOutput();
            tb_numOfPoint.Text = temp.GetNumberOfPoints().ToString();
        }

        private void 增加结点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            增加结点ToolStripMenuItem.Checked = !增加结点ToolStripMenuItem.Checked;
            MousePick = !MousePick;
            if(MousePick)
                this.HookEvents();
            else this.UnhookEvents();

        }

        /// <summary>
        /// 对事件进行响应操作。首先在状态栏显示事件、坐标等信息，然后对LeftButtonPressEvent响应，执行pickcell获取三维坐标及cellId
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PrintEvent(Kitware.VTK.vtkObject sender, Kitware.VTK.vtkObjectEventArgs e)
        {
            vtkRenderWindowInteractor Interactor = renderWindowControl1.RenderWindow.GetInteractor();
            int[] pos = Interactor.GetEventPosition();
            string keysym = Interactor.GetKeySym();
            sbyte keycode = Interactor.GetKeyCode();

            string line = String.Format("{0} ({1},{2}) ('{3}',{4}) {5} data='0x{6:x8}'{7}",
              Kitware.VTK.vtkCommand.GetStringFromEventId(e.EventId),
              pos[0], pos[1],
              keysym, keycode,
              e.Caller.GetClassName(), e.CallData.ToInt32(), System.Environment.NewLine);

            System.Diagnostics.Debug.Write(line);
            //this.textEvents.AppendText(line);
            state.Text = line;

            if (MousePick||拉伸工具ToolStripMenuItem.Checked)
            {
                Kitware.VTK.vtkRenderer CurrentRender = Interactor.FindPokedRenderer(pos[0], pos[1]);
                Interactor.GetPicker().Pick(pos[0], pos[1], 0.0, this.renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer());
                //Kitware.VTK.vtkAbstractPropPicker picker=Kitware.VTK.vtkAbstractPropPicker.New();
                picker = Kitware.VTK.vtkCellPicker.SafeDownCast(Interactor.GetPicker());
                Kitware.VTK.vtkAssemblyPath path = vtkAssemblyPath.New();
                path = picker.GetPath();

                if (path != null)
                {
                    pickedId = picker.GetCellId();
                    posit = picker.GetPickPosition();
                    picker.Pick(posit[0], posit[1], posit[2], CurrentRender);
                    if ((Kitware.VTK.vtkCommand.EventIds)e.EventId == Kitware.VTK.vtkCommand.EventIds.LeftButtonPressEvent)
                    {
                        label3.Text = posit[0].ToString() + "\r\n" + posit[1].ToString() + "\r\n" + posit[2].ToString() + "\r\ncellId:" + pickedId.ToString();
                        //this.AddtextActor(pos, posit);
                        //this.Interactor.EndPickCallback();
                        AddPoint(posit, pickedId);
                    }
                }
            }


        }



        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //事件触发机制。鼠标或键盘交互触发事件。这里触发PrintEvent()函数事件
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public void HookEvents()
        {
            vtkRenderWindowInteractor Interactor = renderWindowControl1.RenderWindow.GetInteractor();
            this.HookErrorWindowEvents();

            Interactor = this.renderWindowControl1.RenderWindow.GetInteractor();
            this.InteractorHandler = new Kitware.VTK.vtkObject.vtkObjectEventHandler(Interactor_AnyEventHandler);
            Interactor.AnyEvt += this.InteractorHandler;
            // this.Interactor.PickEvt+=new Kitware.VTK.vtkObject.vtkObjectEventHandler(Interactor_AnyEventHandler);

            // Give our own style a higher priority than the built-in one
            // so that we see the events first:
            //
            float builtInPriority = Interactor.GetInteractorStyle().GetPriority();

            this.UserStyle = Kitware.VTK.vtkInteractorStyleUser.New();
            this.UserStyle.SetPriority(0.5f);
            this.UserStyle.SetInteractor(Interactor);

            this.UserHandler = new Kitware.VTK.vtkObject.vtkObjectEventHandler(UserStyle_MultipleEventHandler);

            // Keyboard events:
            this.UserStyle.KeyPressEvt += this.UserHandler;
            this.UserStyle.CharEvt += this.UserHandler;
            this.UserStyle.KeyReleaseEvt += this.UserHandler;
        }

        public void UnhookEvents()
        {
            vtkRenderWindowInteractor Interactor = renderWindowControl1.RenderWindow.GetInteractor();
            this.UserStyle.KeyPressEvt -= this.UserHandler;
            this.UserStyle.CharEvt -= this.UserHandler;
            this.UserStyle.KeyReleaseEvt -= this.UserHandler;

            Interactor.AnyEvt -= this.InteractorHandler;

            this.UserHandler = null;
            this.UserStyle = null;
            this.InteractorHandler = null;
            Interactor = null;
        }

        private void HookErrorWindowEvents()
        {
            if (null == this.ErrorWindow)
            {
                this.ErrorWindow = Kitware.VTK.vtkOutputWindow.GetInstance();
                this.ErrorHandler = new Kitware.VTK.vtkObject.vtkObjectEventHandler(ErrorWindow_ErrorHandler);

                this.ErrorWindow.ErrorEvt += this.ErrorHandler;
            }
        }

        void ErrorWindow_ErrorHandler(Kitware.VTK.vtkObject sender, Kitware.VTK.vtkObjectEventArgs e)
        {
            string s = "unknown";
            if (e.CallData != IntPtr.Zero)
            {
                s = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(e.CallData);
            }

            System.Diagnostics.Debug.Write(System.String.Format(
              "ErrorWindow_ErrorHandler called: sender='{0}' e='{1}' s='{2}'", sender, e, s));
        }

        void UserStyle_MultipleEventHandler(Kitware.VTK.vtkObject sender, Kitware.VTK.vtkObjectEventArgs e)
        {
            vtkRenderWindowInteractor Interactor = renderWindowControl1.RenderWindow.GetInteractor();
            string keysym = Interactor.GetKeySym();

            Kitware.VTK.vtkCommand.EventIds eid = (Kitware.VTK.vtkCommand.EventIds)e.EventId;

            switch (eid)
            {
                case Kitware.VTK.vtkCommand.EventIds.KeyPressEvent:
                case Kitware.VTK.vtkCommand.EventIds.CharEvent:
                case Kitware.VTK.vtkCommand.EventIds.KeyReleaseEvent:
                    if (keysym == "f")
                    {
                        // Temporarily disable the interactor, so that the built-in 'f'
                        // handler does not get called:
                        //
                       Interactor.Disable();

                        // Turn on the timer, so we can re-enable the interactor
                        // after the processing of this event is over (one tenth
                        // of a second later...)
                        //
                        this.timer1.Enabled = true;
                    }
                    break;
            }

            this.PrintEvent(sender, e);
        }

        void Interactor_AnyEventHandler(Kitware.VTK.vtkObject sender, Kitware.VTK.vtkObjectEventArgs e)
        {
            this.PrintEvent(sender, e);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            vtkRenderWindowInteractor Interactor = renderWindowControl1.RenderWindow.GetInteractor();
            // Re-enable the interactor:
            //
            Interactor.Enable();

            // Disable the timer, so it's not continually firing:
            //在UserStyle_MultipleEventHandler中，如果按键‘f’,会停止交互，通过定时器恢复
            this.timer1.Enabled = false;

        }

        //****************************************************************************************






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //在原mesh上增加鼠标点击获取的点：posit为点的三维坐标；PickedCellId为获取的点所在cell的id
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// 在原mesh上增加鼠标点击获取的点：posit为点的三维坐标；PickedCellId为获取的点所在cell的id
        /// </summary>
        /// <param name="posit">点的三维坐标</param>
        /// <param name="pickedCellId">点所在cell的id</param>
        private void AddPoint(double[] posit,int pickedCellId)
        {
            vtkPolyData input = vtkPolyData.SafeDownCast(originalMesh);
            vtkPolyData output = vtkPolyData.New();
            
            int numPts, numCells;
            vtkPoints outputPts;
            vtkCellArray outputPolys;
            vtkPointData outputPD;
            vtkCellData outputCD;
            vtkIntArray edgeData;

            numPts = input.GetNumberOfPoints();
            numCells = input.GetNumberOfCells();
            if (numPts < 1 || numCells < 1)
            {
                MessageBox.Show("No data to interpolate!","错误",MessageBoxButtons.OK);
            }
            //
            //Initialize and check input
            //
            vtkPolyData inputDS = vtkPolyData.New();
            inputDS.CopyStructure(input);
            inputDS.GetPointData().PassData(input.GetPointData());
            inputDS.GetCellData().PassData(input.GetCellData());
            //check for triangle in input; if none,stop execution
            inputDS.BuildLinks(numPts);
            vtkCellArray polys = inputDS.GetPolys();
            int hasTris = 0;
            int numCellPts = 0;
            int i = 0;
            inputDS.GetPolys().InitTraversal();
            vtkIdList pts=vtkIdList.New();
            while(inputDS.GetPolys().GetNextCell(pts)!=0)
            {
                i++;
                numCellPts = pts.GetNumberOfIds();
                if (numCellPts == 3)
                {
                    if(inputDS.IsTriangle(pts.GetId(0),pts.GetId(1),pts.GetId(2))!=0)
                    {
                        hasTris=1;
                    }
                }
            }
            if (hasTris==0)
            {
                MessageBox.Show("only operates on triangles,but this data operate on is not a triangle", "提示", MessageBoxButtons.OK);
            }

            //copy points from input.The new points will include the old points
            //and points depend on mouse click
            outputPts = vtkPoints.New();
            outputPts.GetData().DeepCopy(inputDS.GetPoints().GetData());

            //copy point data structure from input
            outputPD = vtkPointData.New();
            outputPD.CopyAllocate(inputDS.GetPointData(), inputDS.GetNumberOfPoints() + 1,1000);

            //copy celldata structure from input
            outputCD = vtkCellData.New();
            outputCD.CopyAllocate(inputDS.GetCellData(), numCells + 2,1000);

            //create triangles
            outputPolys = vtkCellArray.New();
            outputPolys.Allocate(outputPolys.EstimateSize(numCells + 2, 3),1000);

            //create an array to hold new location indices
            edgeData = vtkIntArray.New();
            edgeData.SetNumberOfComponents(3);
            edgeData.SetNumberOfTuples(numCells);

            //循环遍历，更新并找出pickedCell，并更新
            vtkPoints inputPts=inputDS.GetPoints();
            vtkPointData inputPD=inputDS.GetPointData();
            vtkCellData inputCD = inputDS.GetCellData();

            //GenerateSubdivisionPointAndCells
            inputDS.GetPolys().InitTraversal();
            for (int cellId = 0;inputDS.GetPolys().GetNextCell(pts)!=0;cellId++)
            {
                int npts = pts.GetNumberOfIds();
                //int type = inputDS.GetCellType(cellId);
                if (inputDS.GetCellType(cellId) != 5)//VTK_TRIANGLE
                {
                    continue;
                }

                int p1 = pts.GetId(0);
                int p2 = pts.GetId(1);
                int p3 = pts.GetId(2);

                //copy pointData(原points已经拷贝纸outputPts)
                outputPD.CopyData(inputPD, p1, p1);
                outputPD.CopyData(inputPD, p2, p2);
                outputPD.CopyData(inputPD, p3, p3);
                //Do we need to create a point in this cell?
                if (cellId == pickedCellId)
                {
                    //insert new point and pointData
                    int newId = outputPts.InsertNextPoint(posit[0],posit[1],posit[2]);
                    int nnpts = outputPts.GetNumberOfPoints();
                    outputPD.CopyData(inputCD,p1,newId);
                    //pts.SetNumberOfIds(3);
                    //pts.SetId(0,p1);
                    //pts.SetId(1,p2);
                    //pts.SetId(2,p3);
                    //double[] weights=new double[3]{0.5,0.5,0.5};
                    //outputPD.InterpolatePoint(inputPD, newId, pts,);

                    //insert 3 cell and cellData
                    vtkTriangle newCell = vtkTriangle.New();
                    newCell.GetPointIds().SetId(0, p1);
                    newCell.GetPointIds().SetId(1, p2);
                    newCell.GetPointIds().SetId(2, newId);
                    int newCellId = outputPolys.InsertNextCell(newCell);
                    outputCD.CopyData(inputCD, cellId, newCellId);
                    int ncells = outputPolys.GetNumberOfCells();

                    newCell.GetPointIds().SetId(0, p2);
                    newCell.GetPointIds().SetId(1, p3);
                    newCell.GetPointIds().SetId(2, newId);
                    newCellId = outputPolys.InsertNextCell(newCell);
                    outputCD.CopyData(inputCD, cellId, newCellId);

                    newCell.GetPointIds().SetId(0, p3);
                    newCell.GetPointIds().SetId(1, p1);
                    newCell.GetPointIds().SetId(2, newId);
                    newCellId = outputPolys.InsertNextCell(newCell);
                    outputCD.CopyData(inputCD, cellId, newCellId);

                    //int[] newCellPts= new int[3];
                    //int id=0;
                    //newCellPts[id++]=p1;
                    //newCellPts[id++]=p2;
                    //newCellPts[id++]=newId;
                    //int newCellId=outputPolys.InsertNextCell(3,newCellPts);
                    //outputCD.CopyData(inputCD,cellId,newCellId);

                    //int id=0;
                    //newCellPts[id++]=p2;
                    //newCellPts[id++]=p3;
                    //newCellPts[id++]=newId;
                    //int newCellId=outputPolys.InsertNextCell(3,newCellPts);
                    //outputCD.CopyData(inputCD,cellId,newCellId);

                    //int id=0;
                    //newCellPts[id++]=p3;
                    //newCellPts[id++]=p1;
                    //newCellPts[id++]=newId;
                    //int newCellId=outputPolys.InsertNextCell(3,newCellPts);
                    //outputCD.CopyData(inputCD,cellId,newCellId);
                }
                else
                {
                    //insert original cell and cell Data;
                    vtkTriangle newCell = vtkTriangle.New();
                    newCell.GetPointIds().SetId(0, p1);
                    newCell.GetPointIds().SetId(1, p2);
                    newCell.GetPointIds().SetId(2, p3);
                    int newCellId = outputPolys.InsertNextCell(newCell);
                    outputCD.CopyData(inputCD, cellId, newCellId);
                }

            }
            output.SetPoints(outputPts);
            output.SetPolys(outputPolys);

            int n1 = outputPolys.GetNumberOfCells();
            int num1 = originalMesh.GetNumberOfCells();
            originalMesh.DeepCopy(output);
            int num2 = originalMesh.GetNumberOfCells();
            mapper.SetInput(originalMesh);
            renderWindow.Render();

            tb_numOfPoint.Text = originalMesh.GetNumberOfPoints().ToString();
        }

        //*********************************************************************************************



        
        private void 拉伸工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            拉伸工具ToolStripMenuItem.Checked = !拉伸工具ToolStripMenuItem.Checked;
            if (拉伸工具ToolStripMenuItem.Checked)
                this.HookEvents();
            else this.UnhookEvents();
        }






    }
}
