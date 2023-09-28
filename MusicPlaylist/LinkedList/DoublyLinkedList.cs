using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlaylist.LinkedList
{
    public class DoublyLinkedList
    {
        private Node head;
        private Node tail;
        private Node current;
        private Node previous;


        //Add a new node to the end of the list
        public void AddNode(string songName, string artistName, TimeSpan duration)
        {
            Song song = new Song
            {
                Title = songName, 
                Artist = artistName, 
                Duration = duration 
            };

            Node newNode = new Node(song);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
                current = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }
        }

        //Remove a node from the list
        public void RemoveNode(string songName)
        {
            Node current = head;

            while (current != null)
            {
                if (current.Data.Title == songName)
                {
                    if (current == head)
                    {
                        head = current.Next;
                        current.Next.Previous = null;
                    }
                    else if (current == tail)
                    {
                        tail = current.Previous;
                        current.Previous.Next = null;
                    }
                    else
                    {
                        current.Previous.Next = current.Next;
                        current.Next.Previous = current.Previous;
                    }

                    if (current == this.current)
                        this.current = current.Next;

                    return;
                }
                current = current.Next;
            }
        }

        //Get the next node in the list
        public Song GetNextNode()
        {
            if (current == null)
            {
                if(head != null)
                {
                    current = head.Next;
                    return head.Data;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                previous = current.Previous;
                Song song = current.Data;
                current = current.Next;               
                return song;
            }
        }

        //Get the previous node in the list
        public Song GetPreviousNode()
        {
            if (previous == null)
            {
                return null;
            }
            else
            {
                Song song = previous.Data;
                previous = previous.Previous;
                if(current != null)
                {
                    current = current.Previous;
                }
                else
                {
                    current = previous;
                }
                
                return song;
            }
        }


        //Get the first node in the list
        public Song GetFirstNode()
        {
            if (head == null)
            {
                return null;
            }
            else
            {
                return head.Data;
            }
        }

        //Get the last node in the list
        public Song GetLastNode()
        {
            if (tail == null)
            {
                return null;
            }
            else
            {
                return tail.Data;
            }
        }

        //set the current node to the first node in the list
        public void ResetCurrent()
        {
            current = head;
        }

        //Shuffle the list
        public void Shuffle()
        {
            Random random = new Random();
            int count = 0;
            Node current = head;

            //Count the number of nodes in the list
            while (current != null)
            {
                count++;
                current = current.Next;
            }

            current = head;

            while (current != null)
            {
                int randomIndex = random.Next(0, count);
                Node randomNode = head;

                for (int i = 0; i < randomIndex; i++)
                {
                    randomNode = randomNode.Next;
                }

                Song temp = current.Data;
                current.Data = randomNode.Data;
                randomNode.Data = temp;

                current = current.Next;
            }

        }
    }
}
