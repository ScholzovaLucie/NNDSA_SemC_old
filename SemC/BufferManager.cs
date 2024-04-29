using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemC
{
    public class BufferManager
    {
        private Buffer buffer1;
        private Buffer buffer2;
        private bool useBuffer1;
        private bool displayContents;
        private HeapFileManager fileManager;

        public BufferManager(HeapFileManager fileManager)
        {
            this.fileManager = fileManager;
            buffer1 = new Buffer();
            buffer2 = new Buffer();
            useBuffer1 = true;
        }
        public void LoadNextBlock(int blockIndex)
        {
            Block block = fileManager.ReadBlockFromFile(blockIndex);
            if (useBuffer1)
            {
                buffer1.LoadBlock(block);
                useBuffer1 = false;
            }
            else
            {
                buffer2.LoadBlock(block);
                useBuffer1 = true;
            }
        }

        public void ProcessNextBlock(int blockIndex)
        {
            Block block = fileManager.ReadBlockFromFile(blockIndex);
            Buffer targetBuffer = useBuffer1 ? buffer1 : buffer2;
            targetBuffer.LoadBlock(block);
            if (displayContents)
            {
                targetBuffer.DisplayContents();
            }
            useBuffer1 = !useBuffer1; 
        }

        internal void DisplayCurrentBufferContents()
        {
            Buffer currentBuffer = useBuffer1 ? buffer2 : buffer1;
            if (currentBuffer.CurrentBlock != null)
            {
                currentBuffer.DisplayContents();
            }
            else
            {
                Console.WriteLine("Aktuální buffer je prázdný.");
            }
        }

        internal void WriteBufferToFile(int bufferIndex)
        {
            Buffer targetBuffer = bufferIndex == 1 ? buffer1 : buffer2;
            if (targetBuffer != null && targetBuffer.CurrentBlock != null && targetBuffer.CurrentBlock.Records.Count > 0)
            {
                fileManager.WriteBlockToFile(targetBuffer.CurrentBlock);
            }
            else
            {
                throw new InvalidOperationException("Buffer je prázdný nebo nebyl načten.");
            }
        }
    }
}
